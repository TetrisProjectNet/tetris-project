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

namespace Tetris;

public partial class GamePage : ContentPage
{
    private int[,] GameGrid = new int[20, 10];
    private TetrisPiece[] tetrisPieces = new TetrisPiece[]
    {
        new SPiece(),
        new IPiece(),
        new LPiece(),
        new TPiece(),
        new ZPiece(),
        new OPiece(),
        new JPiece(),
    };

    private readonly string[] FullPieces = new string[]
    {
        "fullgreen.png",
        "fulllightblue.png",
        "fullorange.png",
        "fullpurple.png",
        "fullred.png",
        "fullyellow.png",
        "fulldarkblue.png",
    };

    private TetrisPiece _currentPiece;
    private BlockPosition _currentOffset = new(3, 0);
    private Random random = new Random();
    private int _nextId = -1;
    private bool _switchAvailable = true;
    private bool rotationHold;
    private bool escapeHold;
    private int _points;
    private int _clearedRows;
    private int _heldPieceId = -1;
    private bool _gameOver;
    private bool _gameRunning;
    private bool _gamePaused;
    private bool _gameQuit;
    private bool _gameStart = true;

    public static Point GetAppWindowPosition() {
        var window = Application.Current.Windows[0];
        var x = ((IWindow)window).X + 8;
        var y = ((IWindow)window).Y + 8;
        return new Point(x, y);
    }
    public GamePage()
    {
        InitializeComponent();
        NavigationPage.SetHasBackButton(this, false);
        NavigationPage.SetHasNavigationBar(this, false);

        Task.Run(StartGame);
    }

    public async Task StartGame()
    {
        var hook = CreateKeypressListener();
        if (!hook.IsRunning) hook.RunAsync();
        await Task.Run(StartCountBack).ContinueWith(t => {
            _gameStart = false;
        });

        await GameLoop();
    }

    public async Task GameLoop()
    {
        _clearedRows = 0;
        _gameRunning = true;
        _nextId = GetRandomTetrisPieceId(random);
        _currentPiece = tetrisPieces[_nextId];
        _nextId = GetRandomTetrisPieceId(random);
        var nextImage = (Image)FindByName("NextImage");
        await UpdateImageSource(nextImage, _nextId);

        while (!_gameQuit) {
            if (_gameStart) {
                await StartCountBack();
                await UpdateImageSource(nextImage, _nextId);
                _gameRunning = true;
            }
            if (!_gameRunning) continue;
            RedrawTetris();
            await Task.Delay(1000);
            if (_gameRunning) MovePieceDown();
            RedrawTetris();
        }
    }

    private int GetRandomTetrisPieceId(Random random)
    {
        int index = _nextId;
        do {
            _nextId = random.Next(tetrisPieces.Length);
        } while (index == _nextId);

        return _nextId;
    }

    private async Task UpdatePointsAndCoins()
    {
        var pointsDisplay = (Label)FindByName("PointsLabel");
        var coinsDisplay = (Label)FindByName("CoinsLabel");
        await MainThread.InvokeOnMainThreadAsync(async () =>
        {
            pointsDisplay.Text = $"{_clearedRows * 65}";
            coinsDisplay.Text = $"{_clearedRows * 6}";
        });
    }

    private async Task UpdateImageSource(Image image, int id)
    {
        await MainThread.InvokeOnMainThreadAsync(async () =>
        {
            image.Source = FullPieces[id];
        });
    }

    public TaskPoolGlobalHook CreateKeypressListener()
    {
        var hook = SingletonHook.Instance;
        hook.KeyPressed += OnKeyPressed;
        hook.KeyReleased += OnKeyReleased;
        hook.MouseClicked += OnMouseClicked;

        return hook;
    }

    public async Task StartCountBack()
    {
        int delayedSeconds = 0;
        
        if (_nextId != -1) {
            while (delayedSeconds != 5) {
                await Task.Delay(1000);
                RedrawTetris();
                delayedSeconds++;
            }
            _gameStart = false;
            return;
        }

        while (delayedSeconds != 3)
        {
            await Task.Delay(1000);
            RedrawTetris();
            delayedSeconds++;
        }
    }

    void OnKeyReleased(object sender, KeyboardHookEventArgs e)
    {
        if (_gamePaused) {
            if (e.Data.KeyCode == KeyCode.VcEscape) {
                MainThread.BeginInvokeOnMainThread(() => {
                    if (escapeHold) escapeHold = false;
                });
            }
        }
        if (!_gameRunning) return;
        if (e.Data.KeyCode == KeyCode.VcW)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                if (rotationHold) rotationHold = !rotationHold;
            });
        }
    }

    void OnMouseClicked(object sender, MouseHookEventArgs e)
    {
        if (_gamePaused || _gameOver) {
            bool inCanvas = checkForCanvasPress(e);
            if (inCanvas) checkForButtonPress(e);
            return;
        }
        if (!_gameRunning) return;
        if (!_switchAvailable) return;
        if (e.Data.Button == MouseButton.Button2)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                SwitchHeldPiece();
                RedrawTetris();
            });
        }
    }

    public bool checkForCanvasPress(MouseHookEventArgs e) {
        if (!(e.Data.Button == MouseButton.Button1)) return false;
        if (IsInBoundingBox(new SKPoint(755, 180), new SKPoint(1166, 1001), e)) return true;
        return false;
    }

    public void checkForButtonPress(MouseHookEventArgs e) {
        if (IsInBoundingBox(new SKPoint(755, 553), new SKPoint(931, 637), e)) {
            if (!_gameOver) {
                _gameRunning = true;
                _gamePaused = false;
                escapeHold = false;
            } else {
                RestartGame();
            }
            return;
        }
        if (IsInBoundingBox(new SKPoint(990, 553), new SKPoint(1166, 637), e)) {
            _gameQuit = true;
            MainThread.BeginInvokeOnMainThread(() => {
                Navigation.PopAsync(false);
            });
            return;
        }
    }

    public bool IsInBoundingBox(SKPoint point, SKPoint point2, MouseHookEventArgs e) {
        Point windowPoint = GetAppWindowPosition();
        if (e.Data.X > point.X + windowPoint.X && e.Data.X < point2.X + windowPoint.X &&
                e.Data.Y > point.Y + windowPoint.Y && e.Data.Y < point2.Y + windowPoint.Y) return true;
        return false;
    }

    void OnKeyPressed(object sender, KeyboardHookEventArgs e)
    {
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
        if (e.Data.KeyCode == KeyCode.VcD)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                bool isValid = isValidRightMove();
                if (isValid)
                {
                    MovePieceRight();
                    RedrawTetris();
                }
            });
        }

        if (e.Data.KeyCode == KeyCode.VcA)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                bool isValid = isValidLeftMove();
                if (isValid) {
                    MovePieceLeft();
                    RedrawTetris();
                }
            });
        }

        if (e.Data.KeyCode == KeyCode.VcS)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                MovePieceDown();
                RedrawTetris();
            });
        }

        if (e.Data.KeyCode == KeyCode.VcW)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                if (!rotationHold) {
                    rotationHold = true;
                    RotatePiece();
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

    public void MovePieceRight()
    {
        if (_currentOffset.X + _currentPiece.Blocks[_currentPiece.stateNumber][3].position.X > 8) return;
        _currentOffset = new(++_currentOffset.X, _currentOffset.Y);
    }
    
    public void MovePieceLeft()
    {
        if (_currentOffset.X + _currentPiece.Blocks[_currentPiece.stateNumber][0].position.X < 1) return;
        _currentOffset = new(--_currentOffset.X, _currentOffset.Y);
    }

    public void MovePieceDown()
    {
        Block[] NextPlace = _currentPiece.Blocks[_currentPiece.stateNumber];
        for (int i = 0; i < _currentPiece.Blocks[_currentPiece.stateNumber].Length; i++) {
            if (NextPlace[i].position.Y + _currentOffset.Y > 18 || isPositionOccupied(NextPlace[i].position, new BlockPosition(0, 1))) {
                PieceHasBeenSet();
                return;
            }
        }
        _currentOffset = new(_currentOffset.X, ++_currentOffset.Y);
    }

    public bool isValidLeftMove()
    {
        Block[] current = _currentPiece.Blocks[_currentPiece.stateNumber];
        for (int i = 0; i < 4 ; i++) {
            if (!isValidGridPosition(current[i].position, new BlockPosition(-1, 0))) return false;
            if (isPositionOccupied(current[i].position, new BlockPosition(-1, 0))) return false;
        }

        return true;
    }

    public bool isValidRightMove()
    {
        Block[] current = _currentPiece.Blocks[_currentPiece.stateNumber];
        for (int i = 0; i < 4; i++)
        {
            if (!isValidGridPosition(current[i].position, new BlockPosition(1, 0))) return false;
            if (isPositionOccupied(current[i].position, new BlockPosition(1, 0))) return false;
        }

        return true;
    }

    public bool isValidGridPosition(BlockPosition position, BlockPosition offset)
    {
        if (position.X + offset.X + _currentOffset.X > 9) return false;
        if (position.X + offset.X + _currentOffset.X < 0) return false;
        return true;
    }

    public void RotatePiece()
    {
        //if (_currentPiece.stateNumber + 1 > 3 || _currentPiece.Blocks.Length == 1) {
        //    _currentPiece.stateNumber = 0;
        //    if (_currentOffset.X + _currentPiece.Blocks[_currentPiece.stateNumber][0].position.X < 0) _currentOffset.X++;
        //    console.Text = _currentPiece.stateNumber.ToString();
        //    return;
        //}
        //_currentPiece.stateNumber++;
        //if (_currentOffset.X + _currentPiece.Blocks[_currentPiece.stateNumber][0].position.X < 0) _currentOffset.X++;
        //console.Text = _currentPiece.stateNumber.ToString();
        bool NextRotationValid = isNextRotationValid();
        if (NextRotationValid) _currentPiece.incrementState();
    }

    public bool isPositionOccupied(BlockPosition position, BlockPosition offset)
    {
        if (GameGrid[position.Y + offset.Y + _currentOffset.Y, position.X + offset.X + _currentOffset.X] == 0) return false;
        return true;
    }
    public bool isPositionOccupied(BlockPosition position)
    {
        if (GameGrid[position.Y + _currentOffset.Y, position.X + _currentOffset.X] == 0) return false;
        return true;
    }

    public bool isNextRotationValid()
    {
        Block[] NextRotation = _currentPiece.Blocks[_currentPiece.previewNextState()];
        int nextMaxW;
        for (int i = 0; i < NextRotation.Length; i++) {
            if (!isValidGridPosition(NextRotation[i].position, new BlockPosition(0, 0))) return false;
            if (isPositionOccupied(NextRotation[i].position)) return false;
            nextMaxW = NextRotation[i].position.X + _currentOffset.X;
            if (nextMaxW == 10)
            {
                switch (NextRotation[i].Id)
                {
                    case 1:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                        if (!isPositionOccupied(NextRotation[i].position, new BlockPosition(-2, 0)))
                        {
                            _currentOffset.X = 7;
                        }

                        break;
                    case 2:
                        if (!isPositionOccupied(NextRotation[i].position, new BlockPosition(-1, 0)))
                        {
                            _currentOffset.X = 6;
                        }

                        break;
                }
            }
            if (nextMaxW == 0)
            {
                switch (NextRotation[i].Id) {
                    case 1:
                    case 3:
                    case 4:
                    case 5:
                    case 7:
                        if (!isPositionOccupied(NextRotation[i].position, new BlockPosition(1, 0))) {
                            _currentOffset.X = 0;
                        }

                        break;
                    case 2:
                        if (!isPositionOccupied(NextRotation[i].position, new BlockPosition(2, 0)))
                        {
                            _currentOffset.X = 0;
                        }

                        break;
                    case 6:
                        return true;

                        break;
                }
            }
        }
                      
        return true;
    }

    public async void SwitchHeldPiece()
    {
        var holdImage = (Image)FindByName("HoldImage");
        var nextImage = (Image)FindByName("NextImage");
        if (_heldPieceId == -1)
        {
            _switchAvailable = false;
            _currentOffset = new BlockPosition(3, 0);
            _heldPieceId = _currentPiece.Blocks[0][0].Id - 1;
            _currentPiece = tetrisPieces[_nextId];
            _nextId = GetRandomTetrisPieceId(random);
            await UpdateImageSource(holdImage, _heldPieceId);
            await UpdateImageSource(nextImage, _nextId);
            return;
        }
        _switchAvailable = false;
        TetrisPiece tmp = _currentPiece;
        _currentPiece = tetrisPieces[_heldPieceId];
        _heldPieceId = tmp.Blocks[0][0].Id - 1;
        await UpdateImageSource(holdImage, _heldPieceId);
    }

    public async void PieceHasBeenSet()
    {
        _switchAvailable = true;
        SaveCurrentPieceData();
        HandleRowManagement();
        if (_currentOffset.X == 3 && _currentOffset.Y == 0) GameOverHandler();
        if (!_gameOver) {
            _currentOffset = new BlockPosition(3, 0);
            _currentPiece = tetrisPieces[_nextId];
            _nextId = GetRandomTetrisPieceId(random);
            await UpdatePointsAndCoins();
            var nextImage = (Image)FindByName("NextImage");
            await UpdateImageSource(nextImage, _nextId);
        }
    }

    public void SaveCurrentPieceData()
    {
        int state = _currentPiece.stateNumber;
        for (int i = 0; i < 4; i++) {
            GameGrid[_currentPiece.Blocks[state][i].position.Y + _currentOffset.Y, _currentPiece.Blocks[state][i].position.X + _currentOffset.X] = _currentPiece.Blocks[state][i].Id;
        }
    }

    public void HandleRowManagement()
    {
        int count = 0;
        for (int i = 0; i < GameGrid.GetLength(0); i++) {
            count = 0;
            for (int j = 0; j < GameGrid.GetLength(1); j++) {
                if (GameGrid[i, j] != 0) count++;
            }

            if (count == 10) {
                ClearRow(i);
                MoveDownGrid(i);
                _clearedRows++;
            }
        }
    }

    public void ClearRow(int row)
    {
        for (int i = 0; i < GameGrid.GetLength(1); i++) {
            GameGrid[row, i] = 0;
        }
    }

    public void ClearGrid() {
        for (int i = 0; i < GameGrid.GetLength(0); i++) {
            for (int j = 0; j < GameGrid.GetLength(1); j++) {
                GameGrid[i, j] = 0;
            }
        }
    }

    public void MoveDownGrid(int row)
    {
        for (int i = row - 1; i > 0; i--) {
            for (int j = 0; j < GameGrid.GetLength(1); j++) {
                GameGrid[i + 1, j] = GameGrid[i, j];
            }
        }
    }

    public void GameOverHandler()
    {
        _gameRunning = false;
        _gameOver = true;
        RedrawTetris();
        SendGameDataToServer();
    }

    public async void RestartGame() {
        ClearGrid();
        _heldPieceId = -1;
        _switchAvailable = true;
        _gamePaused = false;
        _gameOver = false;
        _gameStart = true;
        _switchAvailable = true;
        _clearedRows = 0;
        _heldPieceId = -1;
        _nextId = GetRandomTetrisPieceId(random);
        _currentPiece = tetrisPieces[_nextId];
        _nextId = GetRandomTetrisPieceId(random);

        await MainThread.InvokeOnMainThreadAsync(async () => {
            var nextImage = (Image)FindByName("NextImage");
            var holdImage = (Image)FindByName("HoldImage");
            HoldImage.Source = "transparenttile.png";
            NextImage.Source = "transparenttile.png";
        });
    }

    public async void SendGameDataToServer()
    {
        string oauthToken = await SecureStorage.Default.GetAsync("oauth_token");


    }

    private void RedrawTetris()
    {
        //string imagePath = Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\..\\Game\\Images\\redtile.png");
        //var bitmap = SKBitmap.Decode(imagePath);
        //Resize(new SKImageInfo(70, 70), SKFilterQuality.High);

        //canvas.Clear(new SKColor(7, 7, 7));
        //canvas.DrawBitmap(bitmap, new SKPoint(1, 1));
        //canvas.DrawBitmap(bitmap, new SKPoint(46, 1));
        //for (int i = 0; i < 10; i++)
        //{
        //    canvas.DrawBitmap(bitmap, new SKPoint(1 + i * 41, 1 + i * 41));
        //}

        var canvasView = this.canvasView;
        canvasView.InvalidateSurface();
    }

    private void OnPaintSurface(object sender, SKPaintSurfaceEventArgs args)
    {
        SKCanvas canvas = args.Surface.Canvas;
        TetrisGridDrawable drawable = (TetrisGridDrawable)Resources["tetrisGridDrawable"];
        
        drawable.Draw(canvas, _currentPiece, _currentOffset, GameGrid, _gameOver, _gamePaused, _gameStart);
    }
}