using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.Services {
    public class ShopPictureService {
        private readonly static SKPoint[][] piecePoints = new SKPoint[][] {
            new SKPoint[] { new(0, 40), new(40, 40), new(40, 0), new(80, 0) },
            new SKPoint[] { new(0, 0), new(0, 40), new(40, 40), new(80, 40) },
            new SKPoint[] { new(0, 40), new(40, 40), new(80, 40), new(120, 40) },
            new SKPoint[] { new(0, 40), new(40, 40), new(80, 40), new(80, 0) },
            new SKPoint[] { new(0, 40), new(40, 40), new(40, 0), new(80, 40) },
            new SKPoint[] { new(0, 0), new(40, 0), new(40, 40), new(80, 40) },
            new SKPoint[] { new(40, 0), new(80, 0), new(40, 40), new(80, 40) },
        };

        private readonly static char[] pieceNames = new char[] { 'S', 'J', 'I', 'L', 'T', 'Z', 'O' };

        public ShopPictureService() {

        }

        public void GenerateImage(string path) {
            string outPathBase = Path.Combine(AppContext.BaseDirectory, $"..\\..\\..\\..\\..\\Game\\ShopImages\\full{Path.GetFileName(path).Replace("tile", "000")}");
            string outPathFull;

            try {
                for (int i = 0; i < piecePoints.Length; i++) {
                    outPathFull = outPathBase.Replace("000", pieceNames[i].ToString());
                    if (File.Exists(outPathFull)) continue;
                    var canvas = CreateCanvas();
                    var bitmap = CreateBitmap(canvas, path, piecePoints[i]);
                    SaveImage(bitmap, outPathFull, i);
                }
            } catch (Exception) {

            }
        }

        public void SaveImage(SKBitmap bitmap, string outPathFull, int i) {
            using (SKData data = bitmap.Encode(SKEncodedImageFormat.Png, 100)) {
                byte[] bytes = data.ToArray();
                File.WriteAllBytes(outPathFull, bytes);
            }
        }

        private SKCanvas CreateCanvas() {
            int width = 160;
            int height = 160;
            var info = new SKImageInfo(width, height, SKColorType.Rgba8888, SKAlphaType.Premul);
            var surface = SKSurface.Create(info);
            var canvas = surface.Canvas;
            canvas.Clear(SKColors.Transparent);
            return canvas;
        }

        private SKBitmap CreateBitmap(SKCanvas canvas, string path, SKPoint[] points) {
            SKBitmap bitmap = new SKBitmap(canvas.DeviceClipBounds.Width, canvas.DeviceClipBounds.Height);
            using (SKCanvas bitmapCanvas = new SKCanvas(bitmap)) {
                bitmapCanvas.DrawRect(SKRect.Create(bitmap.Info.Width, bitmap.Info.Height), new SKPaint() { Color = SKColors.Transparent });
                bitmapCanvas.DrawBitmap(SKBitmap.Decode(path), points[0]);
                bitmapCanvas.DrawBitmap(SKBitmap.Decode(path), points[1]);
                bitmapCanvas.DrawBitmap(SKBitmap.Decode(path), points[2]);
                bitmapCanvas.DrawBitmap(SKBitmap.Decode(path), points[3]);
            }

            canvas.DrawBitmap(bitmap, SKPoint.Empty);

            return bitmap;
        }
    }
}
