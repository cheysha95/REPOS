using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogametest.Files.Objects.Entities.Combatants
{
    public class Octorock : Combatant
    {
        public Octorock(ContentManager content, Vector2 vpos) : base(content, vpos)
        {

        }
        public void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

    }
}
