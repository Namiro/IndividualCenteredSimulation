

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MultiAgentSystemV2;

namespace MultiAgentSystem.Helpers.Graphics.Grids
{        /// <summary>
         /// Service to Draw a Grid
         /// </summary>
    public class GridGraphicHelper
    {
        public bool IsDisplayGrid { get; set; } = false;

        private Grid Grid { get; set; }
        private SpriteBatch SpriteBatch { get; set; }

        public GridGraphicHelper() : base()
        {
        }

        public GridGraphicHelper(Grid grid) : base()
        {
            Grid = grid;
        }

        public void Draw(SpriteBatch spriteBatch, Grid grid)
        {
            Grid = grid;

            SpriteBatch = spriteBatch;

            for (int i = 0; i < Grid.XSize; i++)
            {
                for (int j = 0; j < Grid.YSize; j++)
                {
                    FillGridCell(new Coordinate(i, j), ((ICell)Grid.Get(i, j)).Color);
                }
            }

            if (IsDisplayGrid)
                DrawGrid();
        }

        private void DrawGrid()
        {
            int x = 0;
            int y = 0;
            for (int i = 0; i <= Grid.XSize; i++)
            {
                XNAGraphicHelper.DrawLine(SpriteBatch, x, 0, x, App.CanvasSizeY, Color.Black);
                x += App.BoxSize;
            }
            for (int i = 0; i <= Grid.YSize; i++)
            {
                XNAGraphicHelper.DrawLine(SpriteBatch, 0, y, App.CanvasSizeX, y, Color.Black);
                y += App.BoxSize;
            }
        }

        private void FillGridCell(Coordinate coordinate, Color color)
        {
            int x = coordinate.X * App.BoxSize;
            int y = coordinate.Y * App.BoxSize;

            XNAGraphicHelper.DrawCircle(SpriteBatch, new Vector2(x + (App.BoxSize / 2), y + (App.BoxSize / 2)), App.BoxSize / 2, App.BoxSize * 2, color, App.BoxSize);
        }

        private void FillGridCell(Coordinate coordinate)
        {
            // TODO IMAGE
        }

        private void DrawText(Coordinate coordinate, string text)
        {
            // TODO TEXT
        }
    }
}

