using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Timers;
using TiledCS;

namespace Monogametest
{
    public class ObjectManager
    {
        public enum OBJMGRSTATES {Idle, Updating, Drawing, Rebuilding }
        MGRSTATES CURRENTSTATE;

        List<Rectangle> MapBoundries;

        List<GameObject> GameObjects;
        List<Player> players;
        List<Entity> Entities;
       
       // CollisionManager _collisionManager;
        ContentManager content;


        public Player player;

        public ObjectManager(ContentManager content, TiledMap CurrentMap )
        {
            this.content = content;
            player = new Player(content, new Vector2(101, 100)); //DEBUG PLAYER
            var playerManager = new PlayerInputManager(player);

            RoomRebuild(CurrentMap);

            // create collision manager after list is made
            _collisionManager = new CollisionManager(GameObjects);
        }
        
        
        // runs update method on all current objects
        public void Update(GameTime gameTime)
        {
            changeState(MGRSTATES.UPDATING);

            UpdateObjects(gameTime);

            changeState(MGRSTATES.IDLE);
        }


        //Draws all objects to Screen
        public void Draw(SpriteBatch spriteBatch)                                                             
        {
            changeState(MGRSTATES.DRAWING);

            DrawObjects(spriteBatch);

            changeState(MGRSTATES.IDLE);
        }
        

        //Rebuilds object list when room refreshes
        public void RoomRebuild(TiledMap CurrentMap)
        {

            MapBoundries = new List<Rectangle>();
            GameObjects = new List<GameObject>();
            GameObjects.Add(player);
            objectBuilder(CurrentMap);

        }  

        public void UpdateObjects(GameTime gameTime)
        {
            foreach (var item in GameObjects)
            {
                item.Update(gameTime);
            }  //  this works Draw and collision, not for update because everyone has different update methods
            _collisionManager.Update(MapBoundries, GameObjects); // updates collisionmgrs prevlis


        }

        public void DrawObjects(SpriteBatch spriteBatch)
        {
            foreach (var item in GameObjects)
            {
                item.Draw(spriteBatch);
            }
        }


        public void changeState(MGRSTATES State)
        {
            CURRENTSTATE = State;
        }

        public void objectBuilder(TiledMap CurrentMap)
        {
            foreach (var layer in CurrentMap.Layers)
            {
                if (layer.name == "collide")//--------------------------Map Boundries 
                {
                    //get objet list from leyer
                    foreach (var item in layer.objects)
                    {
                        MapBoundries.Add(new Rectangle((int)item.x, (int)item.y, (int)item.width, (int)item.height));
                    }
                }
                if (layer.name == "object")//------------------------Map Objects 
                {
                    foreach (var item in layer.objects)
                    {
                        // if statements to build object depending on specified classtype
                        if (item.@class == "npc")
                        {
                            GameObjects.Add(new Npc(content, new Vector2(item.x, item.y)));
                        }

                    }
                }
            }

        }

    }
}