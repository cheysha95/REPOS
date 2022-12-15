using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection.Metadata;
using A1r.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TiledCS;

// next steps: rentertarget to scale gfx
//make static to access anywhere, not tied to object


namespace Monogametest
{
    public enum MGRSTATES { IDLE,UPDATING,DRAWING}
    public enum Direction { NORTH, SOUTH, EAST, WEST}
    public class Game1 : Game
    {
        public static SpriteBatch _spriteBatch;
        public static GraphicsDeviceManager _graphics;
        public static ScaleManager _scaleManager;
        public static MapManager _mapManager;
        public static InputManager _inputManager;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
           _inputManager = new InputManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.SynchronizeWithVerticalRetrace = true;
            

        }

        protected override void Initialize()
        {
            base.Initialize();
            this.Components.Add(_inputManager);
            Services.AddService(typeof(InputManager), _inputManager);

            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void LoadContent()
        {

            _mapManager = new MapManager(GraphicsDevice, Content);
            _scaleManager = new ScaleManager(GraphicsDevice,_graphics, 240,240);    // 30 tiles * 16px per tile  
           
    
        }

        protected override void Update(GameTime gameTime)
        {
            _mapManager.Update(gameTime);
            _mapManager.ObjectUpdate(gameTime);

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