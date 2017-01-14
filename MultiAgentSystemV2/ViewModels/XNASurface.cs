using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Framework.WpfInterop;
using MultiAgentSystem.Environments;
using MultiAgentSystem.Helpers;
using MultiAgentSystem.Helpers.Grids;
using MultiAgentSystem.Models.Agents;
using MultiAgentSystemV2;
using System.Threading;

namespace MultiAgentSystem.ViewModels
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
        private int i = 0;

        protected override void Initialize()
        {
            Environment = new Environment();

            // must be initialized. required by Content loading and rendering (will add itself to the Services)
            GraphicsDeviceManager = new WpfGraphicsDeviceService(this);
            Content.RootDirectory = "Content";

            // must be called after the WpfGraphicsDeviceService instance was created
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            SpriteBatch = new SpriteBatch(GraphicsDevice);

            Texture2D circleTexture = Content.Load<Texture2D>("circle");

            foreach (Cell cell in Environment.Grid.Grid2D)
            {
                if (cell is Agent)
                {
                    cell.Texture = circleTexture;
                    cell.Color = new Color((float)App.Random.NextDouble(), (float)App.Random.NextDouble(), (float)App.Random.NextDouble());
                    cell.Size = App.BoxSize;
                }

            }
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

                GraphicsDevice.PlatformClear(ClearOptions.Target, Color.White.ToVector4(), 0.0f, 1);
                SpriteBatch.Begin();

                // Clear
                //SpriteBatch.FillRectangle(new Rectangle(0, 0, App.CanvasSizeX, App.CanvasSizeY), Color.White);

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
                    cell.Draw(SpriteBatch);

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
