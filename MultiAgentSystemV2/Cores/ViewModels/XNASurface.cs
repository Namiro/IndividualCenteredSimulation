using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Framework.WpfInterop;
using MonoGame.Framework.WpfInterop.Input;
using MultiAgentSystem.Cores.Helpers;
using MultiAgentSystem.Cores.Helpers.Grids;
using MultiAgentSystem.Cores.Models;
using System.Threading;

namespace MultiAgentSystem.Cores.ViewModels
{
    internal class XNASurface : WpfGame
    {
        private int frameRate = 0;
        private int frameCounter = 0;
        private System.TimeSpan elapsedTime = System.TimeSpan.Zero;

        protected IGraphicsDeviceService GraphicsDeviceManager { get; set; }
        protected SpriteBatch SpriteBatch;
        protected Environment Environment { get; set; }
        protected int TickCount { get; set; } = 0;
        protected Color BackgroundColor = Color.White;
        protected WpfKeyboard Keyboard { get; private set; }
        protected WpfMouse Mouse { get; private set; }

        public static ContentManager ContentManager { get; set; }
        public static SpriteFont Font { get; protected set; }

        /// <summary>
        /// Allow to initialize the System
        /// </summary>
        protected override void Initialize()
        {
            // must be initialized. required by Content loading and rendering (will add itself to the Services)
            GraphicsDeviceManager = new WpfGraphicsDeviceService(this);
            Content.RootDirectory = "Resources";
            Cell.Size = App.BoxSize;
            ContentManager = Content;

            Keyboard = new WpfKeyboard(this);
            Mouse = new WpfMouse(this);

            // must be called after the WpfGraphicsDeviceService instance was created
            base.Initialize();

            try
            { Font = ContentManager.Load<SpriteFont>("spriteFont"); }
            catch (System.Exception ex)
            { Logger.WriteLog(ex.ToString()); }

        }

        /// <summary>
        /// LoadContent will be called once per game/system and is the place to load
        /// all of your content.
        /// It's the place to create and initialize the environement
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            SpriteBatch = new SpriteBatch(GraphicsDevice);
            Environment.Initialize();
        }

        /// <summary>
        /// It will be call at every tick of game/system
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Update(GameTime gameTime)
        {
            if (App.TicksNumber == 0 || TickCount < App.TicksNumber)
            {
                System.DateTime StartCalcul = System.DateTime.Now;
                Environment.Run();
                TickCount++;
                App.Trace("Tick");

                if (App.IsTracedPerformance)
                    Logger.WriteLog("Calcul time : " + System.DateTime.Now.Subtract(StartCalcul).Milliseconds);
            }

            // To calcul the FPS
            elapsedTime += gameTime.ElapsedGameTime;
            if (elapsedTime > System.TimeSpan.FromSeconds(1))
            {
                elapsedTime -= System.TimeSpan.FromSeconds(1);
                frameRate = frameCounter;
                frameCounter = 0;
            }

            Thread.Sleep(App.DelayMilliseconde);
        }

        /// <summary>
        /// Allow to draw the frame. It will be call after each Update 
        /// </summary>
        /// <param name="gameTime"></param>
        protected override void Draw(GameTime gameTime)
        {
            if (!System.Convert.ToBoolean(TickCount % App.RateRefresh))
            {

                GraphicsDevice.Clear(Color.Transparent);
                //GraphicsDevice.PlatformClear(ClearOptions.Target, BackgroundColor.ToVector4(), 0.0f, 1);
                SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, new RasterizerState { ScissorTestEnable = true });

                //SpriteBatch.DrawRectangle(new Rectangle(0, 0, App.CanvasSizeX, App.CanvasSizeY), BackgroundColor * 1f);

                // Draw Grid
                if (App.IsDisplayGrid)
                {
                    int x = 0;
                    for (int i = 0; i <= Environment.Grid.XSize; i++)
                    {
                        XNAGraphicHelper.DrawLine(SpriteBatch, x, 0, x, App.CanvasSizeY, Color.Black);
                        x += App.BoxSize;
                    }
                    int y = 0;
                    for (int i = 0; i <= Environment.Grid.YSize; i++)
                    {
                        XNAGraphicHelper.DrawLine(SpriteBatch, 0, y, App.CanvasSizeX, y, Color.Black);
                        y += App.BoxSize;
                    }
                }

                //Draw All Cells
                foreach (Cell cell in Environment.Grid.Grid2D)
                {
                    cell.Draw(SpriteBatch);

                    if (App.IsDisplayGrid)
                        cell.DrawText(SpriteBatch, cell.DijkstraValue + "", Font);
                }


                // Draw only the Agents
                //foreach (Cell cell in Environment.Agents)
                //    cell.Draw(SpriteBatch);

                base.Draw(gameTime);
                SpriteBatch.End();
            }

            frameCounter++;
            string fps = string.Format("fps: {0} mem : {1}", frameRate, System.GC.GetTotalMemory(false));

            if (App.IsTracedPerformance)
                Logger.WriteLog(fps);
        }
    }
}
