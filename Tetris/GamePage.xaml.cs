using System.Timers;
using Microsoft.Maui.Graphics;
using SkiaSharp;
using SkiaSharp.Views.Maui;
using SkiaSharp.Views.Maui.Controls;
using Tetris.Drawables;
using Tetris.Game;

namespace Tetris;

public partial class GamePage : ContentPage
{
    public GamePage()
    {
        InitializeComponent();
        NavigationPage.SetHasBackButton(this, false);
        NavigationPage.SetHasNavigationBar(this, false);

        Task.Run(GameLoop);
    }

    public async void GameLoop()
    {
        while (true)
        {
            RedrawTetris();
            await Task.Delay(1000);
        }
    }

    private void RedrawTetris()
    {
        //string imagePath = Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\..\\Resources\\Images\\redtile.png");
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
        
        drawable.Draw(canvas);
    }
}