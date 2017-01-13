using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Framework.WpfInterop;
using System;

namespace WpfApplicationtestDrawFast
{
    public class SurfaceGrid : WpfGame
    {

        private int frameRate = 0;
        private int frameCounter = 0;
        private TimeSpan elapsedTime = TimeSpan.Zero;

        private IGraphicsDeviceService GraphicsDeviceManager { get; set; }
        private SpriteBatch spriteBatch;
        private SpriteFont spriteFont;
        private Color alphaColor = Color.White;
        private FPSCounterComponent fps;

        protected override void Initialize()
        {
            // must be called after the WpfGraphicsDeviceService instance was created
            base.Initialize();

            //spriteFont = Content.Load<SpriteFont>("DefaultFont");
            // must be initialized. required by Content loading and rendering (will add itself to the Services)
            GraphicsDeviceManager = new WpfGraphicsDeviceService(this);
        }

        protected override void Update(GameTime gameTime)
        {
            elapsedTime += gameTime.ElapsedGameTime;

            if (elapsedTime > TimeSpan.FromSeconds(1))
            {
                elapsedTime -= TimeSpan.FromSeconds(1);
                frameRate = frameCounter;
                frameCounter = 0;
            }
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

            DrawGrid();



            XNAGraphicHelper.DrawRectangle(spriteBatch, new Rectangle(100, 100, 100, 200), Color.Purple, 20);

            XNAGraphicHelper.DrawCircle(spriteBatch, 400, 100, 90, 3, Color.White * 0.2f);

            XNAGraphicHelper.DrawCircle(spriteBatch, new Vector2(600, 100), 20, 1000, Color.Green, 5);

            frameCounter++;

            string fps = string.Format("fps: {0} mem : {1}", frameRate, GC.GetTotalMemory(false));
            
            base.Draw(gameTime);
            spriteBatch.End();
        }

        private void DrawGrid()
        {
            int x = 0;
            int y = 0;
            for (int i = 0; i <= 20; i++)
            {
                XNAGraphicHelper.DrawLine(spriteBatch, x, 0, x, 600, Color.Black);
                x += 20;
            }
            for (int i = 0; i <= 10; i++)
            {
                XNAGraphicHelper.DrawLine(spriteBatch, 0, y, 600, y, Color.Black);
                y += 20;
            }
        }
    }
}
