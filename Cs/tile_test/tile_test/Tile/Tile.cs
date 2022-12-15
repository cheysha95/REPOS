using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace tile_test

{
    public class Tile
    {
        string textureName;
        public Texture2D texture;

        public int tileWidth = 16;
        public int tileHeight = 16;
        public int posX, posY, posZ;
        public int ID;
        public int gap = 1;

        public List<TileFrame> tileframes;
        public int animationSpeed;
        public double _counter;
        public int animationIndex;

        public Tile(int posx , int posy, int posz,List<TileFrame>TileFrames, int animationSpeed, string TextureName)
        {

 
            this.posX = posx;
            this.posY = posy; 
            this.posZ = posz;
            this.animationSpeed = animationSpeed;
            this.textureName = TextureName;
            this.tileframes = TileFrames;
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>(textureName);
        }

        public void Update(GameTime gameTime)
        {
            if (tileframes.Count <= 1)
            {
                return;
            }

            _counter += gameTime.ElapsedGameTime.TotalMilliseconds;
            if (_counter > animationSpeed)
            {
                _counter = 0;
                animationIndex++;
                if (animationIndex >= tileframes.Count)
                {
                    animationIndex= 0;
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {           // sourceinpixels = posX * tilewidth + (1*posX)
            var sourceRectangle = new Rectangle(tileframes[animationIndex].texturePosX * (tileWidth + gap) + 1, tileframes[animationIndex].texturePosY * (tileHeight + gap) + 1,
                tileWidth, tileHeight);


            var destinationRectangle = new Rectangle(posX * tileWidth, posY * tileHeight, tileWidth, tileHeight);

            spriteBatch.Draw(texture,destinationRectangle,sourceRectangle,Color.White);
        }





    }


}
