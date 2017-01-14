using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MultiAgentSystem.Cores.ViewModels;
using MultiAgentSystem.WatorSystem.Models;

namespace MultiAgentSystem.WatorSystem.ViewModels
{
    internal class XNAWatorGrid : XNASurface
    {

        protected override void LoadContent()
        {
            Environment = new WatorEnvironment();
            BackgroundColor = Color.DarkBlue;

            // Create a new SpriteBatch, which can be used to draw textures.
            SpriteBatch = new SpriteBatch(GraphicsDevice);
        }
    }
}
