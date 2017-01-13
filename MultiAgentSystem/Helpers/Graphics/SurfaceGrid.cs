using C3.XNA;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Framework.WpfInterop;
using System;

namespace WpfApplicationtestDrawFast
{
    public class SurfaceGrid : WpfGame
    {
        private IGraphicsDeviceService GraphicsDeviceManager { get; set; }
        private SpriteBatch spriteBatch;
        private Color alphaColor = Color.White;
        private FPSCounterComponent fps;

        protected override void Initialize()
        {
            // must be called after the WpfGraphicsDeviceService instance was created
            base.Initialize();

            // must be initialized. required by Content loading and rendering (will add itself to the Services)
            GraphicsDeviceManager = new WpfGraphicsDeviceService(this);
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            XNAGraphicHelper.DrawRectangle(spriteBatch, new Rectangle(100, 100, 100, 200), Color.Purple, 20);

            XNAGraphicHelper.DrawCircle(spriteBatch, 400, 100, 90, 3, Color.White * 0.2f);

            XNAGraphicHelper.DrawCircle(spriteBatch, new Vector2(600, 100), 10, 10, Color.Green, 10);


            base.Draw(gameTime);
            spriteBatch.End();
        }
    }
}
