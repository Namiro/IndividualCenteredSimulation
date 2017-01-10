using SharpDX.Direct2D1;
using SharpDX.DirectWrite;
using SharpDX.Mathematics.Interop;

namespace IndividualCenteredSimulation.Helpers
{
    /// <summary>
    /// Service to Draw a Grid
    /// </summary>
    public class GraphicHelperGridSharpDX : D2dControl.D2dControl
    {
        #region Properties

        public bool IsDisplayGrid { get; set; } = false;
        public bool IsDisplayAxeNum { get; set; } = false;
        public Grid Grid { get; set; }


        private int GridStartX { get; set; }
        private int GridStartY { get; set; }
        private RenderTarget RenderTarget { get; set; }
        private bool IsFirst { get; set; } = true;

        private Brush BlackBrush { get; set; }
        #endregion

        #region Construtors

        public GraphicHelperGridSharpDX()
        {

        }

        #endregion

        #region Methods

        public override void Render(RenderTarget target)
        {
            RenderTarget = target;
            if (IsFirst)
            {
                BlackBrush = new SolidColorBrush(RenderTarget, ConvertColor(System.Windows.Media.Colors.Black));

                if (IsDisplayAxeNum)
                    DrawNumCell();

                if (IsDisplayGrid)
                    DrawGrid();

                IsFirst = false;
            }

            RenderTarget.Clear(ConvertColor(System.Windows.Media.Colors.White));
            for (int i = 0; i < Grid.XSize; i++)
            {
                for (int j = 0; j < Grid.YSize; j++)
                {
                    FillGridCell(new Coordinate(i, j), ConvertColor(((IDrawable)Grid.Get(i, j)).Color));
                    //Logger.WriteLog(Newtonsoft.Json.JsonConvert.SerializeObject(ConvertColor(((IDrawable)Grid.Get(i, j)).Color)));
                }
            }

            //RenderTarget.Flush();
        }

        /// <summary>
        /// This method allow to refresh the view.
        /// It means to draw the view in function of the value of MultiAgentSystem.Agents
        /// </summary>
        public void Draw()
        {
            //Render(RenderTarget);
        }

        private void DrawGrid()
        {
            int x = GridStartX;
            int y = GridStartY;
            for (int i = 0; i < Grid.XSize; i++)
            {
                for (int j = 0; j < Grid.YSize; j++)
                {
                    RenderTarget.DrawRectangle(new RawRectangleF(x, y, x + App.BoxSize, y + App.BoxSize), BlackBrush, 1, null);
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

            RenderTarget.FillEllipse(new Ellipse(new RawVector2(x + (App.BoxSize / 2), y + (App.BoxSize / 2)), (App.BoxSize / 2) - 2, (App.BoxSize / 2) - 2), new SolidColorBrush(RenderTarget, color));
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

        #endregion

        #region Tools

        private RawColor4 ConvertColor(System.Windows.Media.Color color)
        {
            return new RawColor4(color.ScR, color.ScG, color.ScB, color.ScA);
        }
        #endregion
    }
}

