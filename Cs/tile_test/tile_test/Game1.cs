using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace tile_test
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public ScaleManager scaleManager;
        public Tile testTile;
        public Texture2D testTexture;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
          //  var test = new Tile();
            base.Initialize();
  
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            scaleManager = new ScaleManager(GraphicsDevice, _graphics, 16, 16); // enter sieze of window

            var testFrame = new TileFrame(0, 0); // controlls the srite(frame) you get
            var testFrame2 = new TileFrame(1, 0);
            var frameList = new List<TileFrame>();
            frameList.Add(testFrame);
            frameList.Add(testFrame2);

            testTexture = Content.Load<Texture2D>("Images\\Player\\player");
 
            testTile = new Tile(0,0,0,frameList,120, 0); // controlls locations drawn


            testTile.LoadContent(testTexture); // load spritesheet



            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {

            testTile.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            //GraphicsDevice.Clear(Color.Blue);
            scaleManager.Begin(GraphicsDevice,_spriteBatch); //maybe make this have its own so i dont have to keep sending it in
            //_spriteBatch.Begin();
            testTile.Draw(_spriteBatch);

            
            scaleManager.Draw(GraphicsDevice,_spriteBatch);
            _spriteBatch.End();
            base.Draw(gameTime);


        }
    }
}