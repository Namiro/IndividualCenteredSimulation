
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MultiAgentSystem.Cores.Helpers.Grids
{
    public abstract class Cell
    {
        private Coordinate _Coordinate;
        public Coordinate Coordinate
        {
            get { return _Coordinate; }
            set
            {
                _Coordinate = value;
                Position = new Vector2(value.X * Size, value.Y * Size);
            }
        }
        public Vector2 Position { get; private set; }
        public virtual Color Color { get; set; } = Color.White;
        public Texture2D Texture { get; set; }
        public static int Size { get; set; }

        public Cell()
        {

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

            Rectangle sourceRectangle = new Rectangle(0, 0, Texture.Width, Texture.Height);
            Vector2 origin = new Vector2(0, 0);

            spriteBatch.Draw(Texture, Position, sourceRectangle, Color, 0f, origin, (float)((1.0f / Texture.Width) * Size), SpriteEffects.None, 0f);
        }
    }
}
