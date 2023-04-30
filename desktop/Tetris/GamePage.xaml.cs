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

namespace Tetris;

public partial class GamePage : ContentPage
{
    private int[,] GameGrid = new int[20, 10];
    private TetrisPiece[] tetrisPieces = new TetrisPiece[]
    {
        new IPiece(),
        new JPiece(),
        new LPiece(),
        new OPiece(),
        new SPiece(),
        new TPiece(),
        new ZPiece(),
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
    private TetrisPiece _nextPiece;
    private BlockPosition _currentOffset = new(3, 0);
    private Random random = new Random();
    private int _nextId = -1;
    private bool _placed;
    private bool rotationHold;
    private int _points;
    private int _clearedRows;
    private bool _gameOver;
    private bool _gameRunning;

    public GamePage()
    {
        InitializeComponent();
        NavigationPage.SetHasBackButton(this, false);
        NavigationPage.SetHasNavigationBar(this, false);

        Task.Run(GameLoop);
    }

    public async void GameLoop()
    {
        CreateKeypressListener();
        await StartCountBack();
        
        await StartGame();
    }

    public async Task StartGame()
    {
        _clearedRows = 0;
        var pointsDisplay = (Label)FindByName("PointsLabel");
        var nextImage = (Image)FindByName("NextImage");
        _gameRunning = true;
        _nextPiece = GetRandomTetrisPiece(random);
        _currentPiece = _nextPiece;
        _nextPiece = GetRandomTetrisPiece(random);

        while (_gameRunning) {
            RedrawTetris();
            await Task.Delay(1000);
            MovePieceDown();
            RedrawTetris();
            await updatePoints(pointsDisplay);
            await updateNextImage(nextImage);
        }
    }

    private TetrisPiece GetRandomTetrisPiece(Random random)
    {
        int index = _nextId;
        do {
            _nextId = random.Next(tetrisPieces.Length);
        } while (index == _nextId);
        
        _placed = false;
        return tetrisPieces[_nextId];
    }

    private async Task updatePoints(Label pointsDisplay)
    {
        await MainThread.InvokeOnMainThreadAsync(async () =>
        {
            pointsDisplay.Text = (_nextId).ToString();
        });
    }

    private async Task updateNextImage(Image image)
    {
        await MainThread.InvokeOnMainThreadAsync(async () =>
        {
            image.Source = FullPieces[_nextId];
        });
    }

    public void CreateKeypressListener()
    {
        var thread = new Thread(() =>
        {
            var hook = new TaskPoolGlobalHook();
            hook.KeyPressed += OnKeyPressed;
            hook.KeyReleased += OnKeyReleased;
            hook.Run();
        });

        thread.IsBackground = true;
        thread.Start();
    }

    public async Task StartCountBack()
    {
        int delayedSeconds = 0;
        while (delayedSeconds != 3)
        {
            await Task.Delay(1000);
            RedrawTetris();
            delayedSeconds++;
        }
    }

    void OnKeyReleased(object sender, KeyboardHookEventArgs e)
    {
        if (e.Data.KeyCode == KeyCode.VcW)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                if (rotationHold) rotationHold = !rotationHold;
            });
        }
    }

    void OnKeyPressed(object sender, KeyboardHookEventArgs e)
    {
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

    public void PieceHasBeenSet()
    {
        _placed = true;
        SaveCurrentPieceData();
        HandleRowManagement();
        if (_currentOffset.X == 3 && _currentOffset.Y == 0) GameOverHandler();
        if (!_gameOver) {
            _currentOffset = new BlockPosition(3, 0);
            _currentPiece = _nextPiece;
            _nextPiece = GetRandomTetrisPiece(random);
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
        
        drawable.Draw(canvas, _currentPiece, _currentOffset, GameGrid, _gameOver);
    }
}