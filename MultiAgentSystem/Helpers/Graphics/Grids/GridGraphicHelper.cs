using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.DirectWrite;
using SharpDX.Mathematics.Interop;

namespace MultiAgentSystem.Helpers.Graphics.Grids
{        /// <summary>
         /// Service to Draw a Grid
         /// </summary>
    public class GridGraphicHelper
    {
        public bool IsDisplayGrid { get; set; } = false;
        public bool IsDisplayAxeNum { get; set; } = false;

        private int GridStartX { get; set; }
        private int GridStartY { get; set; }
        public Grid Grid { get; set; }
        private RenderTarget RenderTarget { get; set; }
        private bool IsFirst { get; set; } = true;

        private SolidColorBrush BlackBrush { get; set; }

        public GridGraphicHelper() : base()
        {
        }

        public void Draw(RenderTarget renderTarget)
        {
            if (Grid == null)
                return;
            Grid = Grid.Clone();
            RenderTarget = renderTarget;
            RenderTarget.Clear(Color.White);

            BlackBrush = new SolidColorBrush(RenderTarget, Color.Black);

            if (IsDisplayAxeNum)
                DrawNumCell();

            if (IsDisplayGrid)
                DrawGrid();


            for (int i = 0; i < Grid.XSize; i++)
            {
                for (int j = 0; j < Grid.YSize; j++)
                {
                    FillGridCell(new Coordinate(i, j), ((ICell)Grid.Get(i, j)).Color);
                }
            }

            Grid = null;
        }

        private void DrawGrid()
        {
            int x = GridStartX;
            int y = GridStartY;
            for (int i = 0; i <= Grid.XSize; i++)
            {
                RenderTarget.DrawLine(new RawVector2(x, 0), new RawVector2(x, WindowGraphicHelper.Form.Size.Height), BlackBrush);
                x += App.BoxSize;
            }
            for (int i = 0; i <= Grid.YSize; i++)
            {
                RenderTarget.DrawLine(new RawVector2(0, y), new RawVector2(WindowGraphicHelper.Form.Size.Width, y), BlackBrush);
                y += App.BoxSize;
            }
        }

        private void DrawNumCell()
        {
            GridStartX = App.BoxSize + 5;
            GridStartY = App.BoxSize + 5;

            int x = GridStartX;
            int y = GridStartY;
            for (int i = 0; i < Grid.XSize; i++)
            {
                RenderTarget.DrawText(i + "", new TextFormat(new SharpDX.DirectWrite.Factory(), "Arial", 12), new RawRectangleF(x, 0 + App.BoxSize / 4, x + App.BoxSize, y + App.BoxSize), BlackBrush);
                x += App.BoxSize;
            }

            for (int i = 0; i < Grid.YSize; i++)
            {
                RenderTarget.DrawText(i + "", new TextFormat(new SharpDX.DirectWrite.Factory(), "Arial", 12), new RawRectangleF(0, y + App.BoxSize / 4, x + App.BoxSize, y + App.BoxSize), BlackBrush);
                y += App.BoxSize;
            }
        }

        private void FillGridCell(Coordinate coordinate, RawColor4 color)
        {
            int x = GridStartX + coordinate.X * App.BoxSize;
            int y = GridStartY + coordinate.Y * App.BoxSize;

            SolidColorBrush solidColorBrush = new SolidColorBrush(RenderTarget, color);
            RenderTarget.FillEllipse(new Ellipse(new RawVector2(x + (App.BoxSize / 2), y + (App.BoxSize / 2)), (App.BoxSize / 2), (App.BoxSize / 2)), solidColorBrush);
            solidColorBrush.Dispose();
        }

        private void FillGridCell(Coordinate coordinate, Bitmap BitmapImage)
        {
            int x = GridStartX + coordinate.X * App.BoxSize;
            int y = GridStartY + coordinate.Y * App.BoxSize;

            RenderTarget.DrawBitmap(BitmapImage, 1, BitmapInterpolationMode.Linear);
        }

        private void FillGridCell(Coordinate coordinate, string text)
        {
            int x = GridStartX + coordinate.X * App.BoxSize;
            int y = GridStartY + coordinate.Y * App.BoxSize;

            RenderTarget.DrawText(text, new TextFormat(new SharpDX.DirectWrite.Factory(), "Arial", 12), new RawRectangleF(x, y + App.BoxSize / 4, x + App.BoxSize, y + App.BoxSize), BlackBrush);
        }

        private void DrawText(Coordinate coordinate, string text)
        {
            //FormattedText formattedText = new FormattedText(text, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Tahoma"), 16, BlackBrush);
        }
    }
}

