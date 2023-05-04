using System.Timers;
using Microsoft.Maui.Graphics;
using SkiaSharp;
using SkiaSharp.Views.Maui;
using SkiaSharp.Views.Maui.Controls;
using Tetris.Drawables;
using Tetris.Game;
using SharpHook;
using SharpHook.Native;
using SharpHook.Reactive;
using System.Threading;
using System.Net.Http.Headers;
using System.Text.Json;
using Tetris.Models;
using System.Text;
using Plugin.Maui.Audio;
using Tetris.References;
using MetroLog;
using Microsoft.Extensions.Logging;

namespace Tetris;

public partial class GamePage : ContentPage {
    private ILogger<GamePage> _logger;
    private GameGrid _gameGrid;

    private bool _switchAvailable = true;
    private bool rotationHold;
    private bool escapeHold;

    private bool _manualDownMoved = false;
    private bool _gameOver;
    private bool _gameRunning;
    private bool _gamePaused;
    private bool _gameQuit;
    private bool _gameStart = true;

    private readonly IAudioManager _audioManager;
    private IAudioPlayer _audioplayer;

    public static Point GetAppWindowPosition() {
        var window = Application.Current.Windows[0];
        var x = ((IWindow)window).X + 8;
        var y = ((IWindow)window).Y + 8;
        return new Point(x, y);
    }
    public GamePage(IAudioManager audioManager, ILogger<GamePage> logger) {
        InitializeComponent();
        NavigationPage.SetHasBackButton(this, false);
        NavigationPage.SetHasNavigationBar(this, false);
        this._audioManager = audioManager;
        this._logger = logger;
        if (_audioplayer is null || !_audioplayer.IsPlaying) PlayBackgroundMusic();

        Task.Run(StartGame);
    }

    public async void PlayBackgroundMusic() {
        var audioPlayer = _audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync("gamebackgroundmusic.mp3"));
        this._audioplayer = audioPlayer;
        audioPlayer.Play();
        audioPlayer.Volume = Preferences.Default.Get("musicvolume", 0.1);
        audioPlayer.Loop = true;
    }

    public async Task StartGame() {
        this._gameGrid = new GameGrid();
        var hook = CreateKeypressListener();
        if (!hook.IsRunning) hook.RunAsync();
        await Task.Run(StartCountBack).ContinueWith(t => {
            _gameStart = false;
        });

        await GameLoop();
    }

    public async Task GameLoop() {
        _gameRunning = true;
        var nextImage = (Image)FindByName("NextImage");
        await UpdateImageSource(nextImage, _gameGrid.NextId);

        while (!_gameQuit) {
            if (_gameStart) {
                await StartCountBack();
                await UpdateImageSource(nextImage, _gameGrid.NextId);
                _gameRunning = true;
            }
            if (!_gameRunning) continue;
            RedrawTetris();
            await Task.Delay(1000);
            if (_gameRunning && !_manualDownMoved) {
                bool pieceSet = _gameGrid.MovePieceDown();
                if (pieceSet) PieceHasBeenSet();
            }
            if (_manualDownMoved) _manualDownMoved = false;
            RedrawTetris();
        }
    }

    private async Task UpdatePointsAndCoins() {
        var pointsDisplay = (Label)FindByName("PointsLabel");
        var coinsDisplay = (Label)FindByName("CoinsLabel");
        await MainThread.InvokeOnMainThreadAsync(async () => {
            pointsDisplay.Text = $"{_gameGrid.ClearedRows * 65}";
            coinsDisplay.Text = $"{_gameGrid.ClearedRows * 6}";
        });
    }
    
    private async Task UpdateImageSource(Image image, int id) {
        string saved = Preferences.Default.Get($"skinsUsed", "0000000");
        char skinUsed = saved[ShopItemIdReferences.SettingsIdToTile[id]];
        int skinId = Preferences.Default.Get($"skinSlot{ShopItemIdReferences.SettingsIdToTile[id]}", -1);

        await MainThread.InvokeOnMainThreadAsync(async () => {
            if (skinId == -1 || skinUsed == '0') {
                image.Source = _gameGrid.fullPieces[id];
                return;
            }
            var imageString = ShopItemIdReferences._ShopImageReferences[skinId].Image;
            // _logger.LogInformation($"{ShopItemIdReferences.defaultSettingItems[id].Ending}, {ShopItemIdReferences._ShopImageReferences[skinId].Image}, skinid:{skinId}, id: {id}, {saved}, {skinUsed}");
            image.Source = imageString.Replace(".png", ShopItemIdReferences.defaultSettingItems[ShopItemIdReferences.SettingsIdToTile[id]].Ending);
        });
    }

    public TaskPoolGlobalHook CreateKeypressListener() {
        var hook = SingletonHook.Instance;
        hook.KeyPressed += OnKeyPressed;
        hook.KeyReleased += OnKeyReleased;
        hook.MouseClicked += OnMouseClicked;

        return hook;
    }

    public async Task StartCountBack() {
        int delayedSeconds = 0;

        if (_gameGrid.NextId != -1) {
            while (delayedSeconds != 5) {
                await Task.Delay(1000);
                RedrawTetris();
                delayedSeconds++;
            }
            _gameStart = false;
            return;
        }

        while (delayedSeconds != 3) {
            await Task.Delay(1000);
            RedrawTetris();
            delayedSeconds++;
        }
    }

    void OnKeyReleased(object sender, KeyboardHookEventArgs e) {
        if (_gamePaused) {
            if (e.Data.KeyCode == KeyCode.VcEscape) {
                MainThread.BeginInvokeOnMainThread(() => {
                    if (escapeHold) escapeHold = false;
                });
            }
        }
        if (!_gameRunning) return;
        if (e.Data.KeyCode == KeyCode.VcW) {
            MainThread.BeginInvokeOnMainThread(() => {
                if (rotationHold) rotationHold = !rotationHold;
            });
        }
    }

    void OnMouseClicked(object sender, MouseHookEventArgs e) {
        //if (_gamePaused || _gameOver) {
        //    bool inCanvas = checkForCanvasPress(e);
        //    if (inCanvas) checkForButtonPress(e);
        //    return;
        //}
        if (!_gameRunning) return;
        if (!_switchAvailable) return;
        if (e.Data.Button == MouseButton.Button2) {
            MainThread.BeginInvokeOnMainThread(() => {
                SwitchHeldPiece();
                RedrawTetris();
            });
        }
    }

    //public bool checkForCanvasPress(MouseHookEventArgs e) {
    //    if (!(e.Data.Button == MouseButton.Button1)) return false;
    //    if (IsInBoundingBox(new SKPoint(755, 180), new SKPoint(1166, 1001), e)) return true;
    //    return false;
    //}

    //public void checkForButtonPress(MouseHookEventArgs e) {
    //    if (IsInBoundingBox(new SKPoint(755, 553), new SKPoint(931, 637), e)) {
    //        if (!_gameOver) {
    //            _gameRunning = true;
    //            _gamePaused = false;
    //            escapeHold = false;
    //        } else {
    //            RestartGame();
    //        }
    //        return;
    //    }
    //    if (IsInBoundingBox(new SKPoint(990, 553), new SKPoint(1166, 637), e)) {
    //        _gameQuit = true;
    //        MainThread.BeginInvokeOnMainThread(() => {
    //            _audioplayer.Stop();
    //            Navigation.PopAsync(false);
    //        });
    //        return;
    //    }
    //}

    //public bool IsInBoundingBox(SKPoint point, SKPoint point2, MouseHookEventArgs e) {
    //    Point windowPoint = GetAppWindowPosition();
    //    if (e.Data.X > point.X + windowPoint.X && e.Data.X < point2.X + windowPoint.X &&
    //            e.Data.Y > point.Y + windowPoint.Y && e.Data.Y < point2.Y + windowPoint.Y) return true;
    //    return false;
    //}

    public void checkForButtonPress(Point? inPoint) {
        if (inPoint is null) return;
        if (IsInBoundingBox(new SKPoint(0, 373), new SKPoint(175, 455), inPoint)) {
            if (!_gameOver) {
                _gameRunning = true;
                _gamePaused = false;
                escapeHold = false;
            } else {
                RestartGame();
            }
            return;
        }
        if (IsInBoundingBox(new SKPoint(236, 373), new SKPoint(410, 455), inPoint)) {
            _gameQuit = true;
            MainThread.BeginInvokeOnMainThread(() => {
                _audioplayer.Stop();
                Navigation.PopAsync(false);
            });
            return;
        }
    }

    public bool IsInBoundingBox(SKPoint point, SKPoint point2, Point? inPoint) {
        if (inPoint.Value.X > point.X && inPoint.Value.X < point2.X &&
                inPoint.Value.Y > point.Y && inPoint.Value.Y < point2.Y) return true;
        return false;
    }

    void OnKeyPressed(object sender, KeyboardHookEventArgs e) {
        if (_gamePaused && !escapeHold) {
            if (e.Data.KeyCode == KeyCode.VcEscape) {
                MainThread.BeginInvokeOnMainThread(() => {
                    if (!_gameOver) {
                        _gamePaused = !_gamePaused;
                        _gameRunning = true;
                    }
                    RedrawTetris();
                });
            }
        }
        if (!_gameRunning) return;
        if (e.Data.KeyCode == KeyCode.VcD) {
            MainThread.BeginInvokeOnMainThread(() => {
                bool isValid = _gameGrid.isValidRightMove();
                if (isValid) {
                    _gameGrid.MovePieceRight();
                    RedrawTetris();
                }
            });
        }

        if (e.Data.KeyCode == KeyCode.VcA) {
            MainThread.BeginInvokeOnMainThread(() => {
                bool isValid = _gameGrid.isValidLeftMove();
                if (isValid) {
                    _gameGrid.MovePieceLeft();
                    RedrawTetris();
                }
            });
        }

        if (e.Data.KeyCode == KeyCode.VcS) {
            MainThread.BeginInvokeOnMainThread(() => {
                _manualDownMoved = true;
                bool pieceSet = _gameGrid.MovePieceDown();
                if (pieceSet) PieceHasBeenSet();
                RedrawTetris();
            });
        }

        if (e.Data.KeyCode == KeyCode.VcW) {
            MainThread.BeginInvokeOnMainThread(() => {
                if (!rotationHold) {
                    rotationHold = true;
                    _gameGrid.RotatePiece();
                    RedrawTetris();
                }
            });
        }

        if (e.Data.KeyCode == KeyCode.VcEscape) {
            MainThread.BeginInvokeOnMainThread(() => {
                if (!_gameOver) {
                    _gamePaused = !_gamePaused;
                    _gameRunning = !_gameRunning;
                    escapeHold = true;
                }
                RedrawTetris();
            });
        }
    }

    public async void SwitchHeldPiece() {
        var holdImage = (Image)FindByName("HoldImage");
        var nextImage = (Image)FindByName("NextImage");
        if (_gameGrid.HeldPieceId == -1) {
            _switchAvailable = false;
            _gameGrid.SwitchFirst();
            await UpdateImageSource(holdImage, _gameGrid.HeldPieceId);
            await UpdateImageSource(nextImage, _gameGrid.NextId);
            return;
        }
        _switchAvailable = false;
        _gameGrid.Switch();
        await UpdateImageSource(holdImage, _gameGrid.HeldPieceId);
    }

    public async void PieceHasBeenSet() {
        PlayGameSound("gamepieceplaced.wav", 200);
        _switchAvailable = true;
        _gameGrid.SaveCurrentPieceData();
        _gameGrid.HandleRowManagement();
        if (_gameGrid.CurrentOffset.X == 3 && _gameGrid.CurrentOffset.Y == 0) GameOverHandler();
        if (!_gameOver) {
            _gameGrid.SetNewBlock();
            await UpdatePointsAndCoins();
            var nextImage = (Image)FindByName("NextImage");
            await UpdateImageSource(nextImage, _gameGrid.NextId);
        }
    }

    public void GameOverHandler() {
        _gameRunning = false;
        _gameOver = true;
        RedrawTetris();
        SendGameDataToServer();
    }

    public async void RestartGame() {
        _gameGrid.ClearGrid();
        _switchAvailable = true;
        _gamePaused = false;
        _gameOver = false;
        _gameStart = true;
        _switchAvailable = true;
        _gameGrid = new GameGrid();
        await UpdatePointsAndCoins();

        await MainThread.InvokeOnMainThreadAsync(async () => {
            var nextImage = (Image)FindByName("NextImage");
            var holdImage = (Image)FindByName("HoldImage");
            HoldImage.Source = "transparenttile.png";
            NextImage.Source = "transparenttile.png";
        });
    }

    private async void SendGameDataToServer() {
        HttpClient httpClient = new HttpClient();
        string oauthToken = await SecureStorage.Default.GetAsync("oauth_token");

        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", oauthToken);

        var response = await httpClient.GetAsync("https://localhost:7041/Auth");

        if (response.IsSuccessStatusCode) {
            var authContent = await response.Content.ReadAsStringAsync();

            UserDTO user = JsonSerializer.Deserialize<UserDTO>(authContent);

            int[] newArray = new int[user.Scores.Length + 1];
            Array.Copy(user.Scores, newArray, user.Scores.Length);
            newArray[newArray.Length - 1] = _gameGrid.ClearedRows * 65;

            user.Scores = newArray;
            user.Coins = user.Coins + _gameGrid.ClearedRows * 6;

            var json = JsonSerializer.Serialize(user);

            var request = new HttpRequestMessage(new HttpMethod("PATCH"), $"https://localhost:7041/User/{user.Id}") {
                Content = new StringContent(json, Encoding.UTF8, "application/json-patch+json")
            };

            response = await httpClient.SendAsync(request);
        }
    }

    private void RedrawTetris() {

        var canvasView = this.canvasView;
        try {
            canvasView.InvalidateSurface();
        } catch (Exception) {

        }
    }

    private void OnPaintSurface(object sender, SKPaintSurfaceEventArgs args) {
        SKCanvas canvas = args.Surface.Canvas;
        TetrisGridDrawable drawable = (TetrisGridDrawable)Resources["tetrisGridDrawable"];

        drawable.Draw(canvas, _gameGrid.CurrentPiece, _gameGrid.CurrentOffset, _gameGrid.Grid, _gameOver, _gamePaused, _gameStart);
    }

    protected override void OnAppearing() {
        base.OnAppearing();

        if (_audioplayer is not null && !_audioplayer.IsPlaying) PlayBackgroundMusic();
    }

    private async void PlayGameSound(string path, int delay) {
        var audioPlayer = _audioManager.CreatePlayer(await FileSystem.OpenAppPackageFileAsync(path));
        audioPlayer.Play();
        audioPlayer.Volume = Preferences.Default.Get("musicvolume", 0.05);
        await Task.Delay(delay);
        audioPlayer.Stop();
        audioPlayer.Dispose();
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e) {
        _logger.LogInformation(e.GetPosition((SKCanvasView)sender).ToString());
        if (_gamePaused || _gameOver) {
            checkForButtonPress(e.GetPosition((SKCanvasView)sender));
            return;
        }
    }
}