using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiaSharp;
using SkiaSharp.Views.Maui;
using SkiaSharp.Views.Maui.Controls;
using Microsoft.Maui.Controls;
using Tetris;

namespace Tetris.Game
{
    public class GameManager
    {
        public async Task initGameAsync(object sender, SKPaintSurfaceEventArgs e)
        {
            SKCanvas canvas = e.Surface.Canvas;

            ClearCanvas(canvas);

            await GameLoop(canvas);

            //string imagePath = Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\..\\Game\\Images\\redtile.png");
        }

        public async Task GameLoop(SKCanvas canvas)
        {
            while (true) {
                await Task.Delay(1000);
                ClearCanvas(canvas);
            }
        }

        public void drawGridTable(SKCanvas canvas)
        {
            SKPaint paint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = new SKColor(22, 22, 22),
                StrokeWidth = 1
            };

            canvas.DrawLine(0, 0, 0, canvas.DeviceClipBounds.Height - 1, paint);
            canvas.DrawLine(0, canvas.DeviceClipBounds.Height - 1, canvas.DeviceClipBounds.Width, canvas.DeviceClipBounds.Height - 1, paint);
            canvas.DrawLine(canvas.DeviceClipBounds.Width - 1, canvas.DeviceClipBounds.Height - 1, canvas.DeviceClipBounds.Width - 1, 0, paint);
            canvas.DrawLine(canvas.DeviceClipBounds.Width - 1, 0, 0, 0, paint);

            for (int i = 0; i < canvas.DeviceClipBounds.Width - 1; i++)
            {
                canvas.DrawLine(i * 41, 0, i * 41, canvas.DeviceClipBounds.Height - 1, paint);
            }

            for (int i = 0; i < canvas.DeviceClipBounds.Height - 1; i++)
            {
                canvas.DrawLine(0, i * 41, canvas.DeviceClipBounds.Width, i * 41, paint);
            }
        }

        public void ClearCanvas(SKCanvas canvas)
        {
            canvas.Clear(new SKColor(7, 7, 7));
            drawGridTable(canvas);
        }
    }
}
