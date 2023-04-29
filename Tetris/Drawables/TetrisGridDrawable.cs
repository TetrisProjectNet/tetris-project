using SkiaSharp;
using SkiaSharp.Views.Maui;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.Drawables
{
    internal class TetrisGridDrawable : SKDrawable
    {
        private int _startCounter = 7;

        public void Draw(SKCanvas canvas)
        {
            ClearCanvas(canvas);
            drawGridTable(canvas);

            if (_startCounter > 0) {
                DrawStartTime(canvas);
                _startCounter--;
                return;
            }
        }

        public void DrawStartTime(SKCanvas canvas)
        {
            SKPaint paint = new SKPaint
            {
                Color = SKColors.White,
                TextAlign = SKTextAlign.Center,
                TextSize = 105,
                Typeface = SKTypeface.FromFamilyName("Tetris")
            };

            if (_startCounter == 1) {
                canvas.DrawText("Go!", canvas.DeviceClipBounds.Width / 2, canvas.DeviceClipBounds.Height / 2, paint);
                return;
            }
            canvas.DrawText($"..{_startCounter - 1}..", canvas.DeviceClipBounds.Width / 2, canvas.DeviceClipBounds.Height / 2, paint);
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
        }
    }
}
