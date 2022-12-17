using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogametest
{
    public class AnimationFrame
    {
        public int textureX;
        public int textureY;
        public AnimationFrame(int x, int y)
        {
            textureX = x + 1;
            textureY = y + 1;
        }
    }
}
