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
        public int objectWidth, objectHeight;
        public Rectangle pos;


        public int ID;
        public string name = "";


        //States
        public Direction currentDirection = Direction.SOUTH;
        public bool isActive = true;
        public bool hasCollision = true;
      

        public GameObject(ContentManager content, Vector2 Vpos, int objectWidth, int objectHeight)
        {
            var testFrame = new AnimationFrame(0, 2); // controlls the srite(frame) you get
            var testFrame2 = new AnimationFrame(1, 2);
            var frameList = new List<AnimationFrame>();
            frameList.Add(testFrame);
            frameList.Add(testFrame2);

            sprite = new Sprite(pos.X,pos.Y,frameList,"Spritesheets\\Player\\Player");
            sprite.LoadContent(content);
            animations = new Dictionary<string, AnimationFrame>();
            
            vectoPos = new Vector2(Vpos.X, Vpos.Y);          
            pos = new Rectangle((int)vectoPos.X, (int)vectoPos.Y, objectWidth, objectHeight);
        }

        public void Update(GameTime gameTime)
        {
   
            //keeps rectangle bound to VectorPOS
            pos.X = (int)vectoPos.X;
            pos.Y = (int)vectoPos.Y;

            //Enables Sprite Animation
            sprite.Update(gameTime, pos.X, pos.Y);

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch);
        }
    }
}




