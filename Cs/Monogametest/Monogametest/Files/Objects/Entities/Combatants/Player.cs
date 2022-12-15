using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using MonoGame.Extended.Content;
using System.Reflection.Metadata;
using Microsoft.Xna.Framework.Input;
using TiledCS;
using MonoGame.Extended.Timers;
using SharpDX.DirectWrite;
using A1r.Input;

namespace Monogametest
{
    public class Player : Combatant
    {
        public KeyboardState previousKeyboardState = Keyboard.GetState();
        public KeyboardState keyboardState = Keyboard.GetState();
        public int hp = 10;
        
       
        public Player(ContentManager content, Vector2 vpos) : base(content, vpos)
        {
            name = "player";
            ID = 1;
           // vectorDir.Y = 1;
            
        }
        public void Update(GameTime gameTime)
        {
            //vectoPos.X = vectoPos.X +10;
           // vectoPos.X = 10; of ocurse its not updating, as onliny gameonbjects are added to a list, obj mgr needs more lists

            base.Update(gameTime); // calls the root update function, UNITS update funtion
        }

        public void Draw(SpriteBatch spriteBatch)
        {


            base.Draw(spriteBatch);
        }
    }
}

