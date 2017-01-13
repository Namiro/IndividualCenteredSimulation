using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Framework.WpfInterop;
using MultiAgentSystem.Environments;
using MultiAgentSystem.Helpers.Graphics.Grids;
using MultiAgentSystemV2;
using System.Threading;

namespace MultiAgentSystem.Helpers.Graphics
{
    public class XNASurface : WpfGame
    {
        private int frameRate = 0;
        private int frameCounter = 0;
        private System.TimeSpan elapsedTime = System.TimeSpan.Zero;

        private IGraphicsDeviceService GraphicsDeviceManager { get; set; }
        private SpriteBatch SpriteBatch;
        private Environment Environment { get; set; }
        private int TickNb { get; set; } = 0;

        public static GridGraphicHelper GridGraphicHelper { get; set; }

        protected override void Initialize()
        {
            Environment = new Environment();

            GridGraphicHelper = new GridGraphicHelper();
            GridGraphicHelper.IsDisplayGrid = App.IsDisplayGrid;



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
            SpriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            if (App.TicksNumber == 0 || TickNb >= App.TicksNumber)
            {
                System.DateTime StartCalcul = System.DateTime.Now;
                Environment.Run();
                TickNb++;
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

        protected override void Draw(GameTime gameTime)
        {
            if (!System.Convert.ToBoolean(TickNb % App.RateRefresh))
            {
                GraphicsDevice.Clear(Color.White);
                SpriteBatch.Begin();

                GridGraphicHelper.Draw(SpriteBatch, Environment.Grid);

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
