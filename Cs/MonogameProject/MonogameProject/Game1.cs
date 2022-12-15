using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TiledCS;

// next steps: rentertarget to scale gfx
//make static to access anywhere, not tied to object


namespace MonoGameProject
{
    public class Game1 : Game
    {
        public static GraphicsDeviceManager _graphics;
        //public static GraphicsDevice _graphicsDevice;
        public static SpriteBatch _spriteBatch;
        public static tiledRenderer _tiledRenderer;
        public static RenderTarget2D _rendertaget;
        public static Scaler screenScale;

        private Player player;
        public static Npc testNpc;

        public static TiledMap _currentMap;
        public static TiledTileset _tileset;
        public static Texture2D _tilesetTexture;
        public static List<GameObject> objectList;
        public static List<Rectangle> mapCollision;
        public static ContentManager content;
        public static objectManager _objectManager;
        public static MapManager _mapManager;
        


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;


        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            content = Content;
            mapCollision = new List<Rectangle>();
            _objectManager = new objectManager();

        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _mapManager = new MapManager(GraphicsDevice);// loading content from pipeline breaks game
            screenScale = new Scaler(GraphicsDevice);

            player = new Player(GraphicsDevice, 0, 0);


            testNpc = new Npc(GraphicsDevice, 128, 120);
            objectList = new List<GameObject>();
            objectList.Add(testNpc);
            objectList.Add(player);
        }

        protected override void Update(GameTime gameTime)
        {
            player.Update(gameTime);

            base.Update(gameTime);

        }

        protected override void Draw(GameTime gameTime)
        {

            screenScale.Begin(GraphicsDevice); // start scaler
            _tiledRenderer.draw(); // draw map
            _objectManager.draw();




            _spriteBatch.Draw(player._texture, player.pos, Color.White);
            _spriteBatch.Draw(testNpc._texture, testNpc.pos, Color.White);


            screenScale.draw(GraphicsDevice); // draw scaler to screen
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}