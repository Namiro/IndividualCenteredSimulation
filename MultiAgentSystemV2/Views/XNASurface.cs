using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Framework.WpfInterop;
using MultiAgentSystem.Environments;
using MultiAgentSystem.Helpers.Graphics.Grids;
using MultiAgentSystemV2;

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
        private Grid Grid { get; set; } = new Grid();

        public static GridGraphicHelper GridGraphicHelper { get; set; }

        protected override void Initialize()
        {
            Environment = new Environment();

            Environment.PropertyChanged += (obj, args) =>
            {
                switch (args.PropertyName)
                {
                    case nameof(Environment.Grid):
                        Grid = Environment.Grid.Clone();
                        break;
                    default:
                        break;
                }
            };

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
            elapsedTime += gameTime.ElapsedGameTime;

            if (elapsedTime > System.TimeSpan.FromSeconds(1))
            {
                elapsedTime -= System.TimeSpan.FromSeconds(1);
                frameRate = frameCounter;
                frameCounter = 0;
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            SpriteBatch.Begin();

            GridGraphicHelper.Draw(SpriteBatch, Grid);

            base.Draw(gameTime);
            SpriteBatch.End();

            frameCounter++;
            string fps = string.Format("fps: {0} mem : {1}", frameRate, System.GC.GetTotalMemory(false));
            //Logger.WriteLog(fps);

            //GraphicsDevice.SetRenderTarget(null);
        }
    }
}
