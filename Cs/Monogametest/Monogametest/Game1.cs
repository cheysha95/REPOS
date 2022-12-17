using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection.Metadata;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TiledCS;

// next steps: rentertarget to scale gfx
//make static to access anywhere, not tied to object


namespace Monogametest
{
    public enum Direction { NULL, NORTH, SOUTH, EAST, WEST}
    public class Game1 : Game
    {
        public static SpriteBatch _spriteBatch;
        public static GraphicsDeviceManager _graphics;
        public static ScreenRenderer _scaleManager;
        public static MapManager _mapManager;
        public static SpriteFont font;
        public static Texture2D debugTexture;


        //Debug texture being drawn by sprite draw.
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.SynchronizeWithVerticalRetrace = true;
        }

        protected override void Initialize()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            debugTexture = new Texture2D(GraphicsDevice,1,1);
            debugTexture.SetData(new[] { Color.White });
            base.Initialize();         
        }

        protected override void LoadContent()
        {
            _mapManager = new MapManager(GraphicsDevice, Content);
            _scaleManager = new ScreenRenderer(GraphicsDevice,_graphics, 240,240);    // 30 tiles * 16px per tile  
            font = Content.Load<SpriteFont>("SpriteFonts\\Arial16");
        }

        protected override void Update(GameTime gameTime)
        {
            _mapManager.Update(gameTime);
            base.Update(gameTime);         
        }

        protected override void Draw(GameTime gameTime)
        {     
            _scaleManager.Begin(GraphicsDevice, _spriteBatch); // start scaler
            _mapManager.Draw(_spriteBatch);

            _scaleManager.Draw(GraphicsDevice,_spriteBatch); // Draw scaler to screen
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}