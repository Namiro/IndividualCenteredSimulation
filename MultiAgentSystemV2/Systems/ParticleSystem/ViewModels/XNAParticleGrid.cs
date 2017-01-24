using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MultiAgentSystem.Cores.ViewModels;
using MultiAgentSystem.ParticleSystem.Models;

namespace MultiAgentSystem.ParticleSystem.ViewModels
{
    internal class XNAParticleGrid : XNASurface
    {
        public XNAParticleGrid() : base()
        {

        }

        protected override void LoadContent()
        {
            Environment = new ParticleEnvironment();

            BackgroundColor = Color.White;

            // Create a new SpriteBatch, which can be used to draw textures.
            SpriteBatch = new SpriteBatch(GraphicsDevice);
        }
    }
}
