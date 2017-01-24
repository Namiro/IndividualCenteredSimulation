
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MultiAgentSystem.Cores.Helpers.Grids
{
    public abstract class Cell
    {
        public static int DijkstraMaxValue = 1;
        private Coordinate _Coordinate;
        public Coordinate Coordinate
        {
            get { return _Coordinate; }
            set
            {
                _Coordinate = value;
                GraphicPosition = new Vector2(value.X * Size, value.Y * Size);
            }
        }
        public Vector2 GraphicPosition { get; private set; }
        public virtual Color Color { get; set; } = Color.White;
        public Texture2D Texture { get; protected set; }
        public static int Size { get; set; }
        public int DijkstraValue { get; set; } = -1;

        public Cell()
        {

        }

        public Cell(Coordinate coordinate)
        {
            Coordinate = coordinate;
        }

        public Cell(Coordinate coordinate, Color color, Texture2D texture, int size)
        {
            Coordinate = coordinate;
            Color = color;
            Texture = texture;
            Size = size;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (this.Texture == null)
                return;

            if (Color == Color.Transparent)
            {
                spriteBatch.Draw(Texture, GraphicPosition, null, null, null, 0, new Vector2((float)((1.0f / Texture.Width) * Size)));
            }
            else
            {
                Rectangle sourceRectangle = new Rectangle(0, 0, Texture.Width, Texture.Height);
                Vector2 origin = new Vector2(0, 0);

                spriteBatch.Draw(Texture, GraphicPosition, sourceRectangle, Color, 0f, origin, (float)((1.0f / Texture.Width) * Size), SpriteEffects.None, 0f);
            }
        }

        public void DrawText(SpriteBatch spriteBatch, string text, SpriteFont font)
        {
            if (this.Texture == null)
                return;

            spriteBatch.DrawString(font, text, GraphicPosition, Color.White, 0f, new Vector2(0, 0), 0.5f, SpriteEffects.None, 0f);
        }
    }
}
