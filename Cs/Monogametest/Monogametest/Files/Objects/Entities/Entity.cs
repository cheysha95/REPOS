using Microsoft.VisualBasic.Devices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogametest
{
    public class Entity : GameObject
    {
        //public enum State { Idle, Walking }
        //public State currentState = State.Idle;



        public string currentAnimation = "IdleSouth";
        public float moveSpeed = .1f;
   

        public Entity(ContentManager content, Vector2 vpos, int id) : base(content, vpos, 16, 16, id)
        {
            Height = 16;Width = 16;
        } 

        public void Update(GameTime gameTime)
        {
            Move(gameTime);

            base.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
           
            base.Draw(spriteBatch);
        }

        public void Move(GameTime gameTime)
        {

            if (vectorDir == Vector2.Zero) { return; }
            vectorDir.Normalize();

            velcoityX = vectorDir.X * (moveSpeed * (float)gameTime.ElapsedGameTime.TotalMilliseconds);
            velcoityY = vectorDir.Y * (moveSpeed * (float)gameTime.ElapsedGameTime.TotalMilliseconds);


            vectorPos.X += velcoityX;
            vectorPos.Y += velcoityY;
            pos.X = (int)Math.Round(vectorPos.X);
            pos.Y = (int)Math.Round(vectorPos.Y);

        }



    }
    //------------------------------------------------------------------------------------

    public class Npc : Entity
    {
        string text;
        public Npc(ContentManager content, Vector2 vpos, int id) : base(content, vpos, id)
        {
            //spriteSheet = content.Load<Texture2D>("SpriteSheets\\link_spriteSheet"); // placeholder
            text = "This is some debug text for a NPC";
            name = "npc";
           

            currentDirection = Direction.EAST;
        }

        public void Update(GameTime gameTime)
        {

            base.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }

}
