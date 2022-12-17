using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Reflection.Metadata;
using Microsoft.Xna.Framework.Input;
using TiledCS;
using System.Drawing.Text;
using Newtonsoft.Json.Schema;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Timers;
using SharpDX.Direct3D9;

namespace Monogametest
{
    public abstract class GameObject
    {
        public Sprite sprite;
        public Dictionary<string, AnimationFrame> animations;

        public Vector2 vectorPos;
        public int Width, Height;
        public Rectangle pos;
        public Vector2 vectorDir;
        public float velcoityX, velcoityY;
        public int moveSpeed;

        public int ID;
        public string name = "";

        //States
        public Direction currentDirection = Direction.SOUTH;
        public bool isActive = true;
        public bool hasCollision = true;
        public bool isColliding;

        public string collisionInfo = "";


        public GameObject(ContentManager content, Vector2 Vpos, int objectWidth, int objectHeight, int id)
        {

            //----------------------------------------------------------------------Placeholder for sprite
            var testFrame = new AnimationFrame(0, 2); // controlls the srite(frame) you get
            var testFrame2 = new AnimationFrame(1, 2);
            var frameList = new List<AnimationFrame>();
            frameList.Add(testFrame);
           frameList.Add(testFrame2);

            sprite = new Sprite(pos.X,pos.Y,frameList,"Spritesheets\\Player\\Player");
            sprite.texture = content.Load<Texture2D>(sprite.textureName);
            animations = new Dictionary<string, AnimationFrame>();

            Width = objectWidth; Height = objectHeight;
            vectorPos = new Vector2(Vpos.X, Vpos.Y);          
            pos = new Rectangle((int)vectorPos.X, (int)vectorPos.Y, Width, Height);
            ID = id;
        }

        public void Update(GameTime gameTime)
        {
            
            //keeps rectangle bound to VectorPOS //is this it?
            pos.X = (int)vectorPos.X;
            pos.Y = (int)vectorPos.Y;

        

            //Enables Sprite Animation
            sprite.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch,vectorPos);
        }
        //--------------------------------------------------------------------





        public void onCollide(Rectangle bounds) // probaly need to streamline this and just get rectangles
        {
        }

        public void onCollide(GameObject gameObject) 
        {
            var collisionVector = pos.GetIntersectionDepth(gameObject.pos);
            int x = (int)collisionVector.X; int y = (int)collisionVector.Y;
            Direction direction = Direction.NULL;

            if (Math.Abs(x) <= 1 & Math.Abs(y) <= 1) { return; } // allows sliding off corners need to fix
            //if (Math.Abs(x) <= 3 & Math.Abs(y) <= 3) { return; }
           // if (Math.Abs(x) > 1 & Math.Abs(y) > 1) { Console.WriteLine(); } // rounding error allowing player entry

            if (x == 1) { direction = Direction.WEST; } //west
            if (x == -1) { direction = Direction.EAST; }// east
            if (y == 1) { direction = Direction.NORTH; } // north
            if (y == -1) { direction = Direction.SOUTH; } //south

            if (direction == Direction.EAST || direction == Direction.WEST) { vectorDir.X = 0; vectorPos.X -= velcoityX; }
            if (direction == Direction.NORTH || direction == Direction.SOUTH) { vectorDir.Y = 0; vectorPos.Y -= velcoityY; }


        }


    }
}




