using MultiAgentSystem.Helpers.Graphics.Grids;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using SharpDX.Windows;
using System.Windows.Forms;
using Device = SharpDX.Direct3D11.Device;
using FactoryD2D = SharpDX.Direct2D1.Factory;
using FactoryDXGI = SharpDX.DXGI.Factory1;

namespace MultiAgentSystem.Helpers.Graphics
{
    public class WindowGraphicHelper : GraphicHelper
    {
        #region Properties

        public RenderTarget RenderTarget { get; set; }
        public static GridGraphicHelper GridGraphicHelper { get; set; }
        public static Form Form { get; set; }

        #endregion

        #region Constructors

        public WindowGraphicHelper()
        {
            GridGraphicHelper = new GridGraphicHelper();
            GridGraphicHelper.IsDisplayAxeNum = App.IsDisplayAxe;
            GridGraphicHelper.IsDisplayGrid = App.IsDisplayGrid;


            // Create render target window
            Form = new RenderForm("MAS");

            // Create swap chain description
            var swapChainDesc = new SwapChainDescription()
            {
                BufferCount = 2,
                Usage = Usage.RenderTargetOutput,
                OutputHandle = Form.Handle,
                IsWindowed = true,
                ModeDescription = new ModeDescription(0, 0, new Rational(60, 1), Format.R8G8B8A8_UNorm),
                SampleDescription = new SampleDescription(1, 0),
                Flags = SwapChainFlags.AllowModeSwitch,
                SwapEffect = SwapEffect.Discard
            };

            // Create swap chain and Direct3D device
            // The BgraSupport flag is needed for Direct2D compatibility otherwise new RenderTarget() will fail!
            Device device;
            SwapChain swapChain;
            Device.CreateWithSwapChain(DriverType.Hardware, DeviceCreationFlags.BgraSupport, swapChainDesc, out device, out swapChain);

            // Get back buffer in a Direct2D-compatible format (DXGI surface)
            Surface backBuffer = Surface.FromSwapChain(swapChain, 0);


            // Create Direct2D factory
            using (var factory = new FactoryD2D())
            {
                // Get desktop DPI
                var dpi = factory.DesktopDpi;

                // Create bitmap render target from DXGI surface
                RenderTarget = new RenderTarget(factory, backBuffer, new RenderTargetProperties()
                {
                    DpiX = dpi.Height,
                    DpiY = dpi.Width,
                    MinLevel = SharpDX.Direct2D1.FeatureLevel.Level_DEFAULT,
                    PixelFormat = new PixelFormat(Format.Unknown, SharpDX.Direct2D1.AlphaMode.Ignore),
                    Type = RenderTargetType.Default,
                    Usage = RenderTargetUsage.None
                });
            }

            // Disable automatic ALT+Enter processing because it doesn't work properly with WinForms
            using (var factory = swapChain.GetParent<FactoryDXGI>())   // Factory or Factory1?
                factory.MakeWindowAssociation(Form.Handle, WindowAssociationFlags.IgnoreAltEnter);

            // Add event handler for ALT+Enter
            Form.KeyDown += (o, e) =>
            {
                if (e.Alt && e.KeyCode == Keys.Enter)
                    swapChain.IsFullScreen = !swapChain.IsFullScreen;
            };

            // Set window size
            Form.Size = new System.Drawing.Size(App.CanvasSizeX, App.CanvasSizeY);

            // Prevent window from being re-sized
            //form.AutoSizeMode = AutoSizeMode.GrowAndShrink;

            // Rendering function
            RenderLoop.Run(Form, () =>
            {
                RenderTarget.BeginDraw();
                RenderTarget.Transform = Matrix3x2.Identity;


                GridGraphicHelper.Draw(RenderTarget);


                RenderTarget.EndDraw();

                swapChain.Present(0, PresentFlags.None);
            });

            RenderTarget.Dispose();
            swapChain.Dispose();
            device.Dispose();
        }

        #endregion

        #region Methods



        #endregion

        #region Static Methods



        #endregion
    }
}
