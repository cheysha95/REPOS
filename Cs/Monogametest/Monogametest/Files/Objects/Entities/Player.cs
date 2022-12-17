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

namespace Monogametest
{
    public class Player : Entity
    {
        public KeyboardState previousKeyboardState = Keyboard.GetState();
        public KeyboardState currentKeyboardState = Keyboard.GetState();
        public int hp = 10;
               
        public Player(ContentManager content, Vector2 vpos, int id) : base(content, vpos, id)
        {
            name = "player";
            ID = 255;  
            previousKeyboardState= Keyboard.GetState();
        }
        public void Update(GameTime gameTime)
        {
            handleInput();
            base.Update(gameTime); // calls the root Update function, UNITS Update funtion
        }

        public void Draw(SpriteBatch spriteBatch) { base.Draw(spriteBatch); }

        //------------------------------------------------
        public void handleInput()
        {
            currentKeyboardState = Keyboard.GetState();  // player needs to check for collision twice wevery movement, once on x and once on y 

            if (currentKeyboardState.IsKeyDown(Keys.W)) { vectorDir.Y = -1; } // set y velocity if pressed
            if (currentKeyboardState.IsKeyUp(Keys.W) & !previousKeyboardState.IsKeyUp(Keys.W)) { vectorDir.Y = 0; } // reset y velocity if let go
            //-------------------------------------------------
            if (currentKeyboardState.IsKeyDown(Keys.S)) { vectorDir.Y = 1; }
            if (currentKeyboardState.IsKeyUp(Keys.S) & !previousKeyboardState.IsKeyUp(Keys.S)) { vectorDir.Y = 0; }
            //--------------------------------------------------
            //----------------------------------------------------
            if (currentKeyboardState.IsKeyDown(Keys.D)) { vectorDir.X = 1; }
            if (currentKeyboardState.IsKeyUp(Keys.D) & !previousKeyboardState.IsKeyUp(Keys.D)) { vectorDir.X = 0; }
            //-----------------------------------------------------
            if (currentKeyboardState.IsKeyDown(Keys.A)) { vectorDir.X = -1; }
            if (currentKeyboardState.IsKeyUp(Keys.A) & !previousKeyboardState.IsKeyUp(Keys.A)) { vectorDir.X = 0; }        
            //-----------------------------------------------------
            if (currentKeyboardState.IsKeyDown(Keys.B))
            { Console.Write("Breakpoint!"); }




            previousKeyboardState = Keyboard.GetState();
        }
    }
}

