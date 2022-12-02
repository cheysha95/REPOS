using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
//using System.Windows.Forms;

namespace Project1
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Texture2D link;
        SpriteFont font;

        Vector2 pos = new Vector2(0,0);

       

        


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            

        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
             link = Content.Load<Texture2D>("linkk");
            font = Content.Load<SpriteFont>("galleryFont");

           

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);



            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
         

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                pos.Y = pos.Y -2;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {

                pos.Y = pos.Y + 2;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {

                pos.X = pos.X - 2;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {

                pos.X = pos.X + 2;
            }



            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            _spriteBatch.Draw(link , pos, Color.White);

            _spriteBatch.DrawString(font, pos.X.ToString(), new Vector2(30, 30), Color.Aqua) ;
            _spriteBatch.DrawString(font, pos.Y.ToString(), new Vector2(30, 70), Color.Aqua);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}