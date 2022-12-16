using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Monogametest

{
    public class ScreenRenderer : MGR
    {
        Rectangle renderTargetDestination;
        RenderTarget2D rendertarget;

        //default size of map is 30*8, so screen size needs to be 240x240
        int windowWidth = 240;
        int windowHeight = 240;
        int scalefactor = 3;

        public ScreenRenderer(GraphicsDevice graphicsDevice, GraphicsDeviceManager graphicsDeviceManager, int windowWidth, int windowHeight)
        {
            //Sets scrren size and applys changes
            graphicsDeviceManager.PreferredBackBufferHeight = windowHeight * scalefactor;
            graphicsDeviceManager.PreferredBackBufferWidth = windowWidth * scalefactor; 
            graphicsDeviceManager.ApplyChanges();

            // Creates rendertarget and generates rendering destination 
            rendertarget = new RenderTarget2D(graphicsDevice, windowWidth, windowHeight, false, graphicsDevice.PresentationParameters.BackBufferFormat, DepthFormat.Depth24);
            renderTargetDestination = new Rectangle(0, 0, windowWidth, windowHeight);
        }

        public void Begin(GraphicsDevice GraphicsDevice, SpriteBatch spriteBatch)
        {
            // can go at begining of draw mehtod to replace spriBatch begin
            //starts drawing to render target
            GraphicsDevice.SetRenderTarget(rendertarget);
            GraphicsDevice.Clear(Color.White); // important
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullCounterClockwise);
        }

        public void Draw(GraphicsDevice graphicsDevice, SpriteBatch spriteBatch)
        {
            //goes at end of draw method, draws rendertarget to defaultRendertarget
            graphicsDevice.SetRenderTarget(null);
            spriteBatch.Draw(rendertarget, renderTargetDestination, Color.White); // draws render target to screen
        }
    }
}