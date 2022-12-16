using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogametest
{
    // Base class for Managers
    public class MGR
    {
        public enum STATE { IDLE, UPDATING, DRAWING}
        public enum LOADSTATE { IDLE, NORTH, SOUTH, EAST, WEST} // should this just be direction?

        STATE CURRENTSTATE;

    }
}
