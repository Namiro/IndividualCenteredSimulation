using SharpDX;
using SharpDX.Direct2D1;
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
        private Grid Grid { get; set; }
        private RenderTarget RenderTarget { get; set; }
        private bool IsFirst { get; set; } = true;

        private SolidColorBrush BlackBrush { get; set; }

        public GridGraphicHelper(Grid grid) : base()
        {
            Grid = grid;
        }

        public void Draw(RenderTarget renderTarget)
        {
            if (IsFirst)
            {
                RenderTarget = renderTarget;

                if (IsDisplayAxeNum)
                    DrawNumCell();

                if (IsDisplayGrid)
                    DrawGrid();

                IsFirst = false;
            }
            for (int i = 0; i < Grid.XSize; i++)
            {
                for (int j = 0; j < Grid.YSize; j++)
                {
                    ;// FillGridCell(new Coordinate(i, j), ((IDrawable)Grid.Get(i, j)).Color);
                }
            }
        }

        private void DrawGrid()
        {
            int x = GridStartX;
            int y = GridStartY;
            for (int i = 0; i < Grid.XSize; i++)
            {
                for (int j = 0; j < Grid.YSize; j++)
                {
                    RenderTarget.DrawRectangle(new RawRectangleF(x, y, App.BoxSize, App.BoxSize), new SolidColorBrush(RenderTarget, Color.Black));
                    //RectangleGeometry rectangleGeometry = new RectangleGeometry(new Rect());
                    //rectangleGeometry.Freeze();
                    //DrawingContext.DrawDrawing(new GeometryDrawing(null, pen, rectangleGeometry));
                    y += App.BoxSize;
                }
                x += App.BoxSize;
                y = GridStartY;
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
                DrawText(new Coordinate(x, 0 + App.BoxSize / 4), i + "");
                x += App.BoxSize;
            }

            for (int i = 0; i < Grid.YSize; i++)
            {
                DrawText(new Coordinate(0, y + App.BoxSize / 4), i + "");
                y += App.BoxSize;
            }
        }

        private void FillGridCell(Coordinate coordinate, Color color)
        {
            int x = GridStartX + coordinate.X * App.BoxSize;
            int y = GridStartY + coordinate.Y * App.BoxSize;

            //EllipseGeometry ellipseGeometry = new EllipseGeometry(new Rect(x + 2, y + 2, App.BoxSize - 4, App.BoxSize - 4));
        }

        private void FillGridCell(Coordinate coordinate, ImageSource imageSource)
        {
            int x = GridStartX + coordinate.X * App.BoxSize;
            int y = GridStartY + coordinate.Y * App.BoxSize;

            //ImageDrawing imageDrawing = new ImageDrawing(imageSource, new Rect(x, y, App.BoxSize, App.BoxSize));
        }

        private void FillGridCell(Coordinate coordinate, string text)
        {
            coordinate.X = GridStartX + coordinate.X * App.BoxSize;
            coordinate.Y = GridStartY + coordinate.Y * App.BoxSize;

            DrawText(coordinate, text);
        }

        private void DrawText(Coordinate coordinate, string text)
        {
            //FormattedText formattedText = new FormattedText(text, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Tahoma"), 16, BlackBrush);
        }
    }
}

