using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogametest
{
    public class Combatant : Entity 
    {
        public Combatant(ContentManager content,Vector2 vpos) : base(content, vpos)
        {
            currentAnimation = currentState.ToString() + currentDirection.ToString();
            currentDirection = Direction.EAST;
        }

        public void Update(GameTime gameTime)
        { // really only the object manager will need gametime and etc
            currentAnimation = currentState.ToString() + currentDirection.ToString();
            base.Update(gameTime);
        } 

        public void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
