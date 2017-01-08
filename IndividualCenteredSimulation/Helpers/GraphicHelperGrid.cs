using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace IndividualCenteredSimulation.Helpers
{
    /// <summary>
    /// Service to Draw a Grid
    /// </summary>
    public class GraphicHelperGrid
    {
        public DrawingImage DrawingImage { get; set; }
        public bool IsDisplayGrid { get; set; } = false;
        public bool IsDisplayAxeNum { get; set; } = false;

        private int GridStartX { get; set; }
        private int GridStartY { get; set; }
        private DrawingGroup DrawingGroup { get; set; }
        private DrawingContext DrawingContext { get; set; }
        private Grid Grid { get; set; }

        private Brush BlackBrush { get; set; } = Brushes.Black;

        public GraphicHelperGrid(Grid grid)
        {
            Grid = grid;

            DrawingGroup = new DrawingGroup();
            DrawingImage = new DrawingImage(DrawingGroup);
            RenderOptions.SetBitmapScalingMode(DrawingImage, BitmapScalingMode.LowQuality);
            RenderOptions.SetEdgeMode­(DrawingImage, EdgeMode.Aliased);

            BlackBrush.Freeze();
        }

        public void Draw()
        {
            DrawingContext = DrawingGroup.Open();
            if (IsDisplayAxeNum)
                DrawNumCell();

            if (IsDisplayGrid)
                DrawGrid();

            for (int i = 0; i < Grid.XSize; i++)
            {
                for (int j = 0; j < Grid.YSize; j++)
                {
                    FillGridCell(new Coordinate(i, j), ((IDrawable)Grid.Get(i, j)).Color);
                }
            }
            DrawingContext.Close();
        }

        private void DrawGrid()
        {
            Pen pen = new Pen(BlackBrush, 1);
            pen.Freeze();

            int x = GridStartX;
            int y = GridStartY;
            for (int i = 0; i < Grid.XSize; i++)
            {
                for (int j = 0; j < Grid.YSize; j++)
                {
                    RectangleGeometry rectangleGeometry = new RectangleGeometry(new Rect(x, y, App.BoxSize, App.BoxSize));
                    rectangleGeometry.Freeze();
                    DrawingContext.DrawDrawing(new GeometryDrawing(null, pen, rectangleGeometry));
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

            EllipseGeometry ellipseGeometry = new EllipseGeometry(new Rect(x + 2, y + 2, App.BoxSize - 4, App.BoxSize - 4));
            Brush brush = new SolidColorBrush(color);
            brush.Freeze();
            ellipseGeometry.Freeze();
            DrawingContext.DrawDrawing(new GeometryDrawing(brush, null, ellipseGeometry));
        }

        private void FillGridCell(Coordinate coordinate, ImageSource imageSource)
        {
            int x = GridStartX + coordinate.X * App.BoxSize;
            int y = GridStartY + coordinate.Y * App.BoxSize;

            ImageDrawing imageDrawing = new ImageDrawing(imageSource, new Rect(x, y, App.BoxSize, App.BoxSize));
            imageDrawing.Freeze();
            DrawingContext.DrawDrawing(imageDrawing);
        }

        private void FillGridCell(Coordinate coordinate, string text)
        {
            coordinate.X = GridStartX + coordinate.X * App.BoxSize;
            coordinate.Y = GridStartY + coordinate.Y * App.BoxSize;

            DrawText(coordinate, text);
        }

        private void DrawText(Coordinate coordinate, string text)
        {
            FormattedText formattedText = new FormattedText(text, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Tahoma"), 16, BlackBrush);
            Geometry geometry = formattedText.BuildGeometry(new Point(coordinate.X, coordinate.Y));
            geometry.Freeze();
            DrawingContext.DrawDrawing(new GeometryDrawing(BlackBrush, null, geometry));
        }
    }
}

