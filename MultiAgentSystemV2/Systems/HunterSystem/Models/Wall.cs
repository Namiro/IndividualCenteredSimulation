using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MultiAgentSystem.Cores.Helpers.Grids;
using MultiAgentSystem.HunterSystem.ViewModels;

namespace MultiAgentSystem.HunterSystem.Models
{
    class Wall : Cell
    {
        public Wall()
        {
            Color = Color.Transparent;
            Texture = XNAHunterGrid.ContentManager.Load<Texture2D>("wall");
        }
    }
}
