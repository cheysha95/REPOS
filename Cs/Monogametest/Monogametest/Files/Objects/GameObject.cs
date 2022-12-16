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

        public Vector2 vectoPos;
        public int Width, Height;
        public Rectangle pos;
        public Vector2 vectorDir;
        public int moveSpeed;


        public int ID;
        public string name = "";


        //States
        public Direction currentDirection = Direction.SOUTH;
        public bool isActive = true;
        public bool hasCollision = true;
      

        public GameObject(ContentManager content, Vector2 Vpos, int objectWidth, int objectHeight, int id)
        {
            var testFrame = new AnimationFrame(0, 2); // controlls the srite(frame) you get
            var testFrame2 = new AnimationFrame(1, 2);
            var frameList = new List<AnimationFrame>();
            frameList.Add(testFrame);
           // frameList.Add(testFrame2);

            sprite = new Sprite(pos.X,pos.Y,frameList,"Spritesheets\\Player\\Player");
            sprite.LoadContent(content);
            animations = new Dictionary<string, AnimationFrame>();

            Width = objectWidth; Height = objectHeight;
            vectoPos = new Vector2(Vpos.X, Vpos.Y);          
            pos = new Rectangle((int)vectoPos.X, (int)vectoPos.Y, Width, Height);
            ID = id;
        }

        public void Update(GameTime gameTime)
        {
   
            //keeps rectangle bound to VectorPOS
            pos.X = (int)Math.Round(vectoPos.X);
            pos.Y = (int)Math.Round(vectoPos.Y);

            //Enables Sprite Animation
            sprite.Update(gameTime, pos.X, pos.Y);


            

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch);
        }
        //--------------------------------------------------------------------





    }
}




