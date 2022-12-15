using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace MonoGameProject
{
    public class objectManager : Game
    {

        public objectManager()
        {

            foreach (var layer in Game1._currentMap.Layers)
            {
                if (layer.name == "collide")//--------------------------
                {
                    //get objet list from leyer
                    foreach (var item in layer.objects)
                    {
                        Game1.mapCollision.Add(new Rectangle((int)item.x, (int)item.y, (int)item.width, (int)item.height));
                    }
                }
                if (layer.name == "objects")//------------------------
                {
                    foreach (var item in layer.objects)
                    {
                        // if statements to build object depending on specified classtype
                        if (item.@class == "npc")
                        {
                            Game1.objectList.Add(new Npc(GraphicsDevice, (int)item.x, (int)item.y));
                        }



                    }
                }


            }
        }

        public void draw()
        {
            foreach (var item in Game1.objectList)
            {
                Game1._spriteBatch.Draw(item._texture, item.pos, Color.White);
            }
        }


    }
}
