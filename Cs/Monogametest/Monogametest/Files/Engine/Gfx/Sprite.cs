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
        string textureName;
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
            //this.posZ = posz;
   
            this.textureName = TextureName;
            this.animationFrames = AnimationFrames;
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>(textureName); // DONT FORET ABOUT ME
        }

        public void Update(GameTime gameTime, int x, int y)
        {
            //updates position
            posX = x; posY = y;

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
        public void Draw(SpriteBatch spriteBatch)
        {           // sourceinpixels = posX * tilewidth + (1*posX)
            var sourceRectangle = new Rectangle(animationFrames[animationIndex].textureX * (tileWidth + gap) + 1, animationFrames[animationIndex].textureY * (tileHeight + gap) + 1,
                tileWidth, tileHeight);


            var destinationRectangle = new Rectangle(posX , posY, tileWidth, tileHeight);

            spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, Color.White);
        }





    }


}
