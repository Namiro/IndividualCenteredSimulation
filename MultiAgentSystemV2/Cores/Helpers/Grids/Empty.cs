using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MultiAgentSystem.Cores.ViewModels;

namespace MultiAgentSystem.Cores.Helpers.Grids
{
    internal class Empty : Cell
    {
        public Empty(Coordinate coordinate) : base(coordinate)
        {
            Color = Color.Black;
            Texture = XNASurface.ContentManager.Load<Texture2D>("square");
            DijkstraValue = int.MaxValue;
        }

        private Color _Color;
        public override Color Color
        {
            get
            {
                if (App.IsDisplayGrid)
                    _Color = Color.Green * (1 - (float)DijkstraValue / DijkstraMaxValue);

                return _Color;
            }

            set
            {
                _Color = value;
            }
        }
    }
}
