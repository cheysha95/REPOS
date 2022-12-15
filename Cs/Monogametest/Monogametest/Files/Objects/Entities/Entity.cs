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
        public enum State { Idle, Walking }
        public State currentState = State.Idle;


        public Vector2 vectorDir;
        public string currentAnimation = "IdleSouth";
        public float moveSpeed = 80;
   

        public Entity(ContentManager content, Vector2 vpos) : base(content, vpos, 16, 16)
        {
            objectHeight = 16;objectWidth = 16;

        } 

        public void Update(GameTime gameTime)
        {
            walk(gameTime);
            Debug.WriteLine("walking");

            base.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
           
            base.Draw(spriteBatch);
        }

        public void walk(GameTime gameTime)
        {

            /*currentState = State.Walking;
            if (direction == Direction.NORTH)
            {
                vectorDir.Y = -1;

            }
            else if (direction == Direction.SOUTH)
            {
                vectorDir.Y = 1;

            }
            else if (direction == Direction.EAST)
            {
                vectorDir.X = 1;

            }
            else if (direction == Direction.WEST)
            {
                vectorDir.X = -1;

            }
            currentState = State.Idle;
            vectorDir = Vector2.Zero;
            */
            //this.vectoPos.X += vectorDir.X * (moveSpeed * (float)gameTime.ElapsedGameTime.TotalMilliseconds);
            //this.vectoPos.Y += vectorDir.Y * (moveSpeed * (float)gameTime.ElapsedGameTime.TotalMilliseconds);
 
        }


    }
    //------------------------------------------------------------------------------------

    public class Npc : Entity
    {
        string text;
        public Npc(ContentManager content, Vector2 vpos) : base(content, vpos)
        {
            //spriteSheet = content.Load<Texture2D>("SpriteSheets\\link_spriteSheet"); // placeholder
            text = "This is some debug text for a NPC";
            name = "npc";
            ID = 2;

            currentDirection = Direction.EAST;
        }
    }

}
