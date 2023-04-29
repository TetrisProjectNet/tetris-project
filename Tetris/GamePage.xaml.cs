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
    private int[,] GameGrid = new int[10, 20];
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

    private TetrisPiece _currentPiece;
    private BlockPosition _currentOffset = new(3, 0);
    private Random random = new Random();
    private int _nextId = -1;
    private bool _placed;

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
        while (true) {
            if (_nextId == -1) _currentPiece = GetRandomTetrisPiece(random);
            RedrawTetris();
            await Task.Delay(1000);
            MovePieceDown();
            RedrawTetris();
        }
    }

    private TetrisPiece GetRandomTetrisPiece(Random random)
    {
        int index = _nextId;
        do {
            _nextId = random.Next(tetrisPieces.Length);
        } while (index == _nextId);
        
        _placed = false;
        _nextId = 0;
        return tetrisPieces[_nextId];
    }

    public void CreateKeypressListener()
    {
        var thread = new Thread(() =>
        {
            var hook = new TaskPoolGlobalHook();
            hook.KeyPressed += OnKeyPressed;
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

    void OnKeyPressed(object sender, KeyboardHookEventArgs e)
    {
        if (e.Data.KeyCode == KeyCode.VcD)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                MovePieceRight();
                RedrawTetris();
            });
        }

        if (e.Data.KeyCode == KeyCode.VcA)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                MovePieceLeft();
                RedrawTetris();
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
                RotatePiece();
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
        if (_currentOffset.Y + 1 > 18) {
            PieceHasBeenSet();
            return;
        }
        _currentOffset = new(_currentOffset.X, ++_currentOffset.Y);
    }

    public void RotatePiece()
    {
        if (_currentPiece.stateNumber + 1 > 3) {
            _currentPiece.stateNumber = 0;
            return;
        }
        _currentPiece.stateNumber++;
    }

    public void PieceHasBeenSet()
    {
        _placed = true;
        SaveCurrentPieceData();
        _currentOffset = new BlockPosition(3, 0);
        _currentPiece = GetRandomTetrisPiece(random);
    }

    public void SaveCurrentPieceData()
    {
        int state = _currentPiece.stateNumber;
        for (int i = 0; i < 4; i++) {
            GameGrid[_currentPiece.Blocks[state][i].position.X + _currentOffset.X, _currentPiece.Blocks[state][i].position.Y + _currentOffset.Y] = _currentPiece.Blocks[state][i].Id;
        }
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
        
        drawable.Draw(canvas, _currentPiece, _currentOffset, GameGrid);
    }
}