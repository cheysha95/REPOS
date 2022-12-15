using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace tile_test

{
    public class AnimatedSprite
    {
        public Texture2D Texture { get; set; } // get texture from parent object, they should have spritesheet. 
        public int Rows;
        public int Columns;
        private int currentFrame;
        private int totalFrames;
        public int timeSinceLastFrame = 0;
        public int millisecondsPerFrame = 110;
        public int startFrame, finishFrame;
        public int spriteWidth = 16, spriteHeight = 16;

        public AnimatedSprite(Texture2D texture, int _startFrame, int _finishFrame)
        {

            //COME BACK HERE TO UPDATE FOR NEW SPRITE SHEETS
            Texture = texture;  
            //Rows = rows;
            Columns = texture.Width / spriteWidth;
            //Columns = columns;
            Rows = texture.Height / spriteHeight;
            currentFrame = _startFrame;
            totalFrames = Rows * Columns;
            startFrame = _startFrame;
            finishFrame = _finishFrame;

        }

        public void Update(GameTime gameTime)
        {
            timeSinceLastFrame += gameTime.ElapsedGameTime.Milliseconds;
            if (timeSinceLastFrame > millisecondsPerFrame)
            {
                timeSinceLastFrame -= millisecondsPerFrame;
                // Increment Current Frame here (See link for implementation) NEED TO STORE ANIMATION IN JSON SOMEHOW
                currentFrame++;
                if (currentFrame > finishFrame)
                    currentFrame = startFrame;
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            int width = Texture.Width / Columns;
            int height = Texture.Height / Rows;
            int row = currentFrame / Columns;
            int column = currentFrame % Columns;

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);

            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);

        }
    }
}