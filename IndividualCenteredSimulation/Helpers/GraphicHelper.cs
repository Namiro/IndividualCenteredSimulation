using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace IndividualCenteredSimulation.Helpers
{
    public static class GraphicHelper
    {
        public static System.Windows.Media.Color CastColor(System.Drawing.Color color)
        {
            return System.Windows.Media.Color.FromArgb(color.A, color.R, color.G, color.B);
        }

        public static BitmapSource ToBitmapSource(this DrawingImage source)
        {
            DrawingVisual drawingVisual = new DrawingVisual();
            DrawingContext drawingContext = drawingVisual.RenderOpen();
            drawingContext.DrawImage(source, new Rect(new Point(0, 0), new Size(source.Width, source.Height)));
            drawingContext.Close();

            RenderTargetBitmap bmp = new RenderTargetBitmap((int)source.Width, (int)source.Height, 96, 96, PixelFormats.Pbgra32);
            bmp.Render(drawingVisual);
            return bmp;
        }

        public static DrawingImage ToDrawingImage(this BitmapSource source)
        {
            Rect imageRect = new Rect(0, 0, source.PixelWidth, source.PixelHeight);
            ImageDrawing drawing = new ImageDrawing(source, imageRect);
            return new DrawingImage(drawing);
        }
    }
}
