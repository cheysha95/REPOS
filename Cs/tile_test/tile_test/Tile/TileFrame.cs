using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tile_test
{
    public class TileFrame
    {
        public int texturePosX;
        public int texturePosY;



        public TileFrame(int texturePosX, int texturePosY) 
        {
            this.texturePosX = texturePosX +1 ; // minus one to make it easier to work with spriteSheet i have
            this.texturePosY = texturePosY +1;

        }


    }
}
