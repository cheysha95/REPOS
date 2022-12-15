using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using TiledCS;



namespace MonoGameProject
{
    public class Scaler : Game
    {

        // SpriteBatch spriteBatch;
        RenderTarget2D rendertarget;
        int windowWidth;
        int windowHeight;
        int scalefactor = 5;

        public Scaler(GraphicsDevice GraphicsDevice)
        {
            windowWidth = Game1._currentMap.Width * Game1._currentMap.TileWidth;
            windowHeight = Game1._currentMap.Height * Game1._currentMap.TileHeight;
            Game1._graphics.PreferredBackBufferHeight = windowHeight * scalefactor;
            Game1._graphics.PreferredBackBufferWidth = windowWidth * scalefactor;
            Game1._graphics.ApplyChanges();

            //spriteBatch = _spriteBatch;
            rendertarget = new RenderTarget2D(
                 GraphicsDevice,
                 windowWidth, windowHeight,
                 false,
                 GraphicsDevice.PresentationParameters.BackBufferFormat,
                 DepthFormat.Depth24);
        }

        public void Begin(GraphicsDevice GraphicsDevice)
        {

            // Game1._rendertaget = rendertarget;
            //changes screen size


            GraphicsDevice.SetRenderTarget(rendertarget);
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //drawing to rendertarget
            Game1._spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend,
            SamplerState.LinearClamp, DepthStencilState.Default,
            RasterizerState.CullNone);
        }

        public void draw(GraphicsDevice graphicsDevice)
        {
            graphicsDevice.SetRenderTarget(null);
            Game1._spriteBatch.Draw(rendertarget, new Rectangle(0, 0, windowWidth, windowHeight), Color.White);
        }

    }
}

