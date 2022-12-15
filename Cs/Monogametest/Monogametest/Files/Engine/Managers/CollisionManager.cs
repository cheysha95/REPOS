using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection.Metadata;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TiledCS;

namespace Monogametest
{
    public class CollisionManager
    {
        public Dictionary<string,Rectangle> PrevLocations = new Dictionary<string, Rectangle>();


        public CollisionManager(List<GameObject> objectList)
        {


            UpdatePrevLocations(objectList);
        }

        public void Update(List<Rectangle> mapCollisionList,List<GameObject> objectList)
        {
            CollisionCheck(mapCollisionList, objectList);



            UpdatePrevLocations(objectList);
        }

        public void UpdatePrevLocations(List<GameObject> objectList)
        {
            PrevLocations = new Dictionary<string, Rectangle>();
            foreach (var item in objectList)
            {
                PrevLocations.Add(item.name, item.pos); // adds everythings location to prev locations dict
            } // i think its working
        }


        public void CollisionCheck(List<Rectangle> mapCollisionList, List<GameObject> objectList)
        {
            foreach (var item in objectList)
            {
                foreach (var itemz in objectList)
                {
                    if (item.pos.Intersects(itemz.pos) & itemz.name != item.name) // and ID
                    {
                       
                        //item.vectorDir.X = 0;
                       // item.pos = PrevLocations[item.name];
                        Console.WriteLine();
                        //COLLIDION

                    }

                }

                if (item.name == "player") // collision for boundries only for player 
                {
                    foreach (var bounds in mapCollisionList)
                    {
                        if (item.pos.Intersects(bounds))
                        {
                            // MAP COLLISION WITH PLAYER

                            //item.pos = PrevLocations[item.name];



                            Console.WriteLine();
                        }
                    }
                }
            }
        }
    }
}
