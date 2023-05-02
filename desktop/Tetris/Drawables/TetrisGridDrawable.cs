using SkiaSharp;
using SkiaSharp.Views.Maui;
using SkiaSharp.Views.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Tetris.Game;

namespace Tetris.Drawables
{
    internal class TetrisGridDrawable : SKDrawable
    {
        private readonly SKBitmap[] pieceImages = new SKBitmap[]
        {
            SKBitmap.Decode(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\..\\Resources\\Images\\transparenttile.png")),
            SKBitmap.Decode(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\..\\Game\\Images\\greentile.png")),
            SKBitmap.Decode(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\..\\Game\\Images\\lightbluetile.png")),
            SKBitmap.Decode(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\..\\Game\\Images\\orangetile.png")),
            SKBitmap.Decode(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\..\\Game\\Images\\purpletile.png")),
            SKBitmap.Decode(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\..\\Game\\Images\\redtile.png")),
            SKBitmap.Decode(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\..\\Game\\Images\\yellowtile.png")),
            SKBitmap.Decode(Path.Combine(AppContext.BaseDirectory, "..\\..\\..\\..\\..\\Game\\Images\\darkbluetile.png"))
        };

        private SKTypeface tetrisFont =  SKTypeface.FromFile(Path.Combine(AppContext.BaseDirectory,
            "..\\..\\..\\..\\..\\Resources\\Fonts\\Tetris.ttf"));

        private int _startCounter = 7;
        public int StartCounter {
            get => _startCounter;
            set => _startCounter = value;
        }

        public void Draw(SKCanvas canvas, TetrisPiece currentPiece, BlockPosition currentOffset, int[,] GameGrid, bool gameOver, bool gamePaused, bool gameStart)
        {
            ClearCanvas(canvas);
            drawGridTable(canvas);

            if (_startCounter > 0 && gameStart) {
                DrawStartTime(canvas);
                _startCounter--;
                return;
            }

            DrawGameGrid(canvas, GameGrid);
            DrawPiece(canvas, currentPiece, currentPiece.stateNumber, currentOffset);
            if (gameOver) {
                DrawGameStopped(canvas, "Game Over");
                DrawGameStoppedButtons(canvas, "Restart");
                _startCounter = 4;
            }
            if (gamePaused) {
                DrawGameStopped(canvas, "Paused");
                DrawGameStoppedButtons(canvas, "Resume");
            }
        }

        public void DrawStartTime(SKCanvas canvas)
        {


            SKPaint paint = new SKPaint
            {
                Color = SKColors.White,
                TextAlign = SKTextAlign.Center,
                TextSize = 105,
                Typeface = tetrisFont,
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

        public void DrawPiece(SKCanvas canvas, TetrisPiece piece, int stateNumber, BlockPosition currentOffset)
        {
            for (int i = 0; i < 4; i++)
            {
                canvas.DrawBitmap(pieceImages[piece.Blocks[stateNumber][i].Id], new SKPoint(1 + (piece.Blocks[stateNumber][i].position.X + currentOffset.X) * 41, 1 + (piece.Blocks[stateNumber][i].position.Y + currentOffset.Y) * 41));
            }
        }

        public void DrawGameGrid(SKCanvas canvas, int[,] GameGrid)
        {
            for (int i = 0; i < GameGrid.GetLength(0); i++) {
                for (int j = 0; j < GameGrid.GetLength(1); j++) {
                    if (GameGrid[i, j] == 0) continue;
                    canvas.DrawBitmap(pieceImages[GameGrid[i, j]], new SKPoint(1 + j * 41, 1 + i * 41));
                }
            }
        }

        public void DrawGameStopped(SKCanvas canvas, string stoppedReason)
        {
            SKPaint rectPaint = new SKPaint();
            rectPaint.Color = new SKColor(11, 11, 12, 230);

            SKPaint paint = new SKPaint
            {
                Color = new SKColor(0, 255, 0),
                TextAlign = SKTextAlign.Center,
                TextSize = 65,
                Typeface = tetrisFont,
            };

            SKPaint backShadow = new SKPaint();
            backShadow.Color = new SKColor(8, 8, 8, 180);

            canvas.DrawRect(-10,
                -10,
                canvas.DeviceClipBounds.Width + 20,
                canvas.DeviceClipBounds.Height + 20,
                backShadow);

            canvas.DrawRect(0,
                canvas.DeviceClipBounds.Height / 4,
                canvas.DeviceClipBounds.Width,
                150,
                rectPaint);

            canvas.DrawText($"{stoppedReason}", canvas.DeviceClipBounds.Width / 2, canvas.DeviceClipBounds.Height / 3 + 30, paint);
        }

        public void DrawGameStoppedButtons(SKCanvas canvas, string stoppedOption) {
            SKPaint rectPaint = new SKPaint();
            rectPaint.Color = new SKColor(11, 11, 12, 230);

            SKPaint paint = new SKPaint {
                Color = new SKColor(0, 255, 0),
                TextAlign = SKTextAlign.Center,
                TextSize = 40,
                Typeface = tetrisFont,
            };

            canvas.DrawRect(canvas.DeviceClipBounds.Width / 2 + 30, // 235
                canvas.DeviceClipBounds.Height / 3 + 100, // 373
                canvas.DeviceClipBounds.Width / 3 + 39, // 156
                85,
                rectPaint);

            canvas.DrawRect(0,
                canvas.DeviceClipBounds.Height / 3 + 100,
                canvas.DeviceClipBounds.Width / 3 + 40,
                85,
                rectPaint);

            canvas.DrawText($"Quit", 325, 429, paint);
            canvas.DrawText($"{stoppedOption}", 89, 429, paint);
        }
    }
}
