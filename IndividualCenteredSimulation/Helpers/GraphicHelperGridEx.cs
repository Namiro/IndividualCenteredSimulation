using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace IndividualCenteredSimulation.Helpers
{
    /// <summary>
    /// Service to Draw a Grid
    /// </summary>
    public class GraphicHelperGridEx
    {
        #region Properties

        public WriteableBitmap WriteableBitmap { get; set; }
        public bool IsDisplayGrid { get; set; } = false;
        public bool IsDisplayAxeNum { get; set; } = false;

        private int GridStartX { get; set; }
        private int GridStartY { get; set; }
        private DrawingGroup DrawingGroup { get; set; }
        private DrawingContext DrawingContext { get; set; }
        private Grid Grid { get; set; }

        #endregion

        #region Construtors

        public GraphicHelperGridEx(Grid grid)
        {
            Grid = grid;

            WriteableBitmap = BitmapFactory.New(Grid.XSize * App.BoxSize + 1, Grid.YSize * App.BoxSize + 1);
        }

        #endregion

        #region Methods

        /// <summary>
        /// This method allow to refresh the view.
        /// It means to draw the view in function of the value of MultiAgentSystem.Agents
        /// </summary>
        public void Draw()
        {
            using (WriteableBitmap.GetBitmapContext())
            {
                // Clear the WriteableBitmap with white color
                WriteableBitmap.Clear(Colors.White);

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
                    WriteableBitmap.DrawRectangle(x, y, x + App.BoxSize, y + App.BoxSize, GraphicHelper.CastColor(System.Drawing.Color.Black));
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
            WriteableBitmap = BitmapFactory.New(Grid.XSize * App.BoxSize + 1 + GridStartX, Grid.YSize * App.BoxSize + 1 + GridStartY);

            int x = GridStartX;
            int y = GridStartY;
            for (int i = 0; i < Grid.XSize; i++)
            {
                LetterGlyphTool.DrawString(WriteableBitmap, x, 0 + App.BoxSize / 4, Colors.Black, new PortableFontDesc(), i + "");
                x += App.BoxSize;
            }

            for (int i = 0; i < Grid.YSize; i++)
            {
                LetterGlyphTool.DrawString(WriteableBitmap, 0, y + App.BoxSize / 4, Colors.Black, new PortableFontDesc(), i + "");
                y += App.BoxSize;
            }
        }

        private void FillGridCell(Coordinate coordinate, Color color)
        {
            int x = GridStartX + coordinate.X * App.BoxSize;
            int y = GridStartY + coordinate.Y * App.BoxSize;

            WriteableBitmap.FillEllipse(x + 2, y + 2, x + App.BoxSize - 2, y + App.BoxSize - 2, color);
        }

        private void FillGridCell(Coordinate coordinate, WriteableBitmap BitmapImage)
        {
            int x = GridStartX + coordinate.X * App.BoxSize;
            int y = GridStartY + coordinate.Y * App.BoxSize;

            // TODO Afficher une image dans une celulle de la grille
        }

        private void FillGridCell(Coordinate coordinate, string text)
        {
            int x = GridStartX + coordinate.X * App.BoxSize;
            int y = GridStartY + coordinate.Y * App.BoxSize;

            LetterGlyphTool.DrawString(WriteableBitmap, x, y + App.BoxSize / 4, Colors.Black, new PortableFontDesc(), text);
        }
        #endregion
    }
}

