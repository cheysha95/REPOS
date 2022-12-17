using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Monogametest

{
    public class Sprite
    {
        public string textureName;
        public Texture2D texture;

        public int tileWidth = 16;
        public int tileHeight = 16;
        public int posX, posY, posZ;
        public int gap = 1;
      
        public List<AnimationFrame> animationFrames;
        public int animationSpeed = 202;
        public double _counter;
        public int animationIndex;

        public Sprite(int posx, int posy, List<AnimationFrame> AnimationFrames, string TextureName)
        {
            this.posX = posx;
            this.posY = posy;
   
            this.textureName = TextureName;
            this.animationFrames = AnimationFrames;
        }

        public void Update(GameTime gameTime)
        {
            //updates animation
            if (animationFrames.Count <= 1)
            {
                return;
            }

            _counter += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (_counter > animationSpeed)
            {
                _counter = 0;
                animationIndex++;
                if (animationIndex >= animationFrames.Count)
                {
                    animationIndex = 0;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 pos)
        {           
            var sourceRectangle = new Rectangle(animationFrames[animationIndex].textureX * (tileWidth + gap) + 1, animationFrames[animationIndex].textureY * (tileHeight + gap) + 1,
                tileWidth, tileHeight);
            var destinationRectangle = new Rectangle((int)pos.X , (int)pos.Y, tileWidth, tileHeight);
           
            //do not delete
           // spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
            spriteBatch.Draw(Game1.debugTexture,destinationRectangle,Color.Black);

        }





    }


}
