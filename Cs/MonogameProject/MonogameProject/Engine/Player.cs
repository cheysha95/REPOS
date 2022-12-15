using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System;

namespace MonoGameProject
{
    public class Player : GameObject
    {

        public Player(GraphicsDevice graphicsDeive, int x, int y) : base(graphicsDeive, x, y)
        {
            Content.RootDirectory = "Content";

            base._texture = Game1.content.Load<Texture2D>("SpriteSheets\\link");
            //base._texture = new Texture2D();
            base.name = "player";
        }
    // CONTENT PIPELINE  TOTALLY BROKEN
        public void Update(GameTime gameTime)
        {

            //objectList = Game1.objectList;
            KeyboardState state = Keyboard.GetState();

            vpos.X = pos.X;
            vpos.Y = pos.Y;
            tilex = (int)vpos.X / Game1._currentMap.TileWidth;
            tiley = (int)vpos.Y / Game1._currentMap.TileHeight;

            // movement and collision
            //------------------------------------------------------
            if (state.IsKeyDown(Keys.W))
            {
                this.pos.Y = this.pos.Y - moveSpeed;
                if (this.pos.Y < 0)
                {
                    Game1._mapManager.Update();
                    this.pos.Y = 0;

                }   //topscreen collision
                if (collision())
                {
                    this.pos.Y = this.pos.Y + moveSpeed;
                }
            }
            //----------------------------------------------------
            if (state.IsKeyDown(Keys.A))
            {
                this.pos.X = this.pos.X - moveSpeed;

                if (this.pos.X < 0)
                {
                    this.pos.X = 0;
                }  //leftscreen collision 
                if (collision())
                {
                    this.pos.X = this.pos.X + moveSpeed;
                }
            }

            //------------------------------------------------------

            if (state.IsKeyDown(Keys.S))
            {
                this.pos.Y = this.pos.Y + moveSpeed;
                if (this.pos.Bottom > Game1._currentMap.Height * Game1._currentMap.TileHeight)
                {
                    this.pos.Y = (Game1._currentMap.Height * Game1._currentMap.TileHeight) - pos.Height;
                }   //bottomscreen collsion
                if (collision())
                {
                    this.pos.Y = this.pos.Y - moveSpeed;
                }

            }
            //-----------------------------------------------------------
            if (state.IsKeyDown(Keys.D))
            {
                this.pos.X = this.pos.X + moveSpeed; //move

                if (this.pos.Right > Game1._currentMap.Width * Game1._currentMap.TileWidth)
                {
                    this.pos.X = (Game1._currentMap.Width * Game1._currentMap.TileWidth) - pos.Width;
                } //rightscreen collision
                if (collision())
                {
                    this.pos.X = this.pos.X - moveSpeed;
                }

            }
            //--------------------------------------------------------------------
            if (state.IsKeyDown(Keys.B)) // breakpoint
            {
                Console.WriteLine("break");
            } //Breakpoint
        }
        public bool collision()
        {
            foreach (var item in Game1.mapCollision)
            {
                if (this.pos.Intersects(item))
                {
                    return true;
                }
            }
            foreach (var item in Game1.objectList)
            {
                if ((this.pos.Intersects(item.pos)) && item.name != "player")
                {
                    return true;
                }
            }
            return false;
        }
    }
}