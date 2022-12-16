using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection.Metadata;
using System.Runtime.Serialization;
using Microsoft.VisualBasic.Logging;
using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Timers;
using TiledCS;

namespace Monogametest
{
    public class MapManager : MGR
    {
        //STATE CURRENTMGRSTATE;
        LOADSTATE CURRENTLOADSTATE;

        string projectPath = Environment.CurrentDirectory + "\\..\\..\\..\\"; //actual path is wonk, get a proper path for project
        List<TiledMap> MapList = new List<TiledMap>();    // list of all maps in directory
        public int mapTileWidth = 3; //mapgrids across, y 
        public TiledTileset currentTileset;
        public Texture2D currentTilesetTexture;
        public TiledMap currentMap;
        public int currentMapIndex;
        public TiledMapLoader tileRenderer;
        public string intersectionDepthtoString = "";  //DEBUG
        public string collisionDirection = ""; //DEBUG
        //-----------------------------------------------------------
        STATE CURRENTSTATE;

        List<Rectangle> MapBoundries;
        List<GameObject> GameObjects;

        public Player player;


        public MapManager(GraphicsDevice graphicsDevice, ContentManager content)
        {
            loadFromDisk(graphicsDevice);

            //THESE NEED TO BE SOUTH HERE
            currentMapIndex = 4; //set spawn room
            currentMap = MapList[currentMapIndex];

            tileRenderer = new TiledMapLoader(graphicsDevice, currentMap, currentTileset, currentTilesetTexture);
            //--------------------------------------------------------------------

            player = new Player(content, new Vector2(10, 10), 255); //DEBUG PLAYER

            mapRebuild(content, currentMap);
        }
        public void Update(ContentManager content, GameTime gameTime)
        {
            ChangeMap(content);
            UpdateObjects(gameTime);
            CollisionCheck();


        }
        public void Draw(SpriteBatch spriteBatch)
        {
            tileRenderer.Draw(spriteBatch);
            DrawObjects(spriteBatch);

            spriteBatch.DrawString(Game1.font, intersectionDepthtoString, new Vector2(0, 0), Color.Black);
            spriteBatch.DrawString(Game1.font, collisionDirection, new Vector2(0, 20), Color.Black);

        }



        public void loadFromDisk(GraphicsDevice graphicsDevice)
        {
            string mapsPath = projectPath + "Content\\Maps\\";
            currentTileset = new TiledTileset(mapsPath + "Oracle_tileset.tsx");
            currentTilesetTexture = Texture2D.FromFile(graphicsDevice, mapsPath + "Oracle_Tileset.png");
            DirectoryInfo d = new DirectoryInfo(mapsPath);
            foreach (var map in d.GetFiles("*.tmx"))
            {
                MapList.Add(new TiledMap(map.FullName)); //adds them to list, 
            }

        }
        public void ChangeMap(ContentManager content) // UpdateMap currentmap, object list, collisionlsit, can add to UpdateMap currentTileset
        {

            if (CURRENTLOADSTATE == LOADSTATE.NORTH)
            {
                currentMapIndex = currentMapIndex - mapTileWidth;
                currentMap = MapList[currentMapIndex];
                mapRebuild(content, currentMap);
                tileRenderer.UpdateMap(currentMap, currentTileset, currentTilesetTexture);
                resetMAPMGRState();
            }
            if (CURRENTLOADSTATE == LOADSTATE.SOUTH)
            {
                currentMapIndex = currentMapIndex + mapTileWidth;
                currentMap = MapList[currentMapIndex];
                mapRebuild(content, currentMap);
                tileRenderer.UpdateMap(currentMap, currentTileset, currentTilesetTexture);
                resetMAPMGRState();
            }
            if (CURRENTLOADSTATE == LOADSTATE.EAST)
            {
                currentMapIndex = currentMapIndex + 1;
                currentMap = MapList[currentMapIndex];
                mapRebuild(content, currentMap);
                tileRenderer.UpdateMap(currentMap, currentTileset, currentTilesetTexture);
                resetMAPMGRState();
            }
            if (CURRENTLOADSTATE == LOADSTATE.WEST)
            {
                currentMapIndex = currentMapIndex - 1;
                currentMap = MapList[currentMapIndex];
                mapRebuild(content, currentMap);
                tileRenderer.UpdateMap(currentMap, currentTileset, currentTilesetTexture);
                resetMAPMGRState();
            }
        }
        public void resetMAPMGRState() { CURRENTLOADSTATE = LOADSTATE.IDLE; }
        //---------------------------------------------------------------------------------------------------------
        public void UpdateObjects(GameTime gameTime)
        {
            foreach (var item in GameObjects)
            {
                item.Update(gameTime);
                //---------------------------calls the child class Update method base on object type, every object type must be added here

                if (item is Player) { playerUpdate(gameTime, (Player)item); } // runs player manager update, then player update
                if (item is Npc) { npcUpdate(gameTime, (Npc)item); }

            }
        }
        public void DrawObjects(SpriteBatch spriteBatch)
        {
            foreach (var item in GameObjects)
            {
                item.Draw(spriteBatch);
            }
        }
        public void loadObjectsFromMap(ContentManager content, TiledMap CurrentMap)
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
                        int t = 0;

                        // if statements to build object depending on specified classtype
                        if (item.@class == "npc")
                        {
                            GameObjects.Add(new Npc(content, new Vector2(item.x, item.y), t));
                        }
                        if (item.@class == "Player")
                        {
                            GameObjects.Add(new Player(content, new Vector2(item.x, item.y), t));
                        }


                        t++;
                    }
                }
            }

        }
        public void mapRebuild(ContentManager content, TiledMap CurrentMap)
        {
            MapBoundries = new List<Rectangle>();
            GameObjects = new List<GameObject>();
            GameObjects.Add(player);
            loadObjectsFromMap(content, CurrentMap);
        }

        //--------------------------------------------------------------------- object Update methods
        public void playerUpdate(GameTime gameTime, Player player) { player.Update(gameTime); }  // runs player manager
        public void npcUpdate(GameTime gameTime, Npc npc) { npc.Update(gameTime); }

        //------------------------------------------------------------------------------------------------------------

        public void CollisionCheck()
        {
            foreach (var obj in GameObjects)
            {
                foreach (var objj in GameObjects)
                {
                    if (obj.pos.Intersects(objj.pos) & obj != objj) // if two rect overlap, and theyre not the same object
                    {
                        //obj on obj collision

                        if (obj is Player)
                        {
                            var collisionVector = obj.pos.GetIntersectionDepth(objj.pos);
                            intersectionDepthtoString = collisionVector.X.ToString() + " , " + collisionVector.Y.ToString();//DEBUG
                            int x = (int)collisionVector.X; int y = (int)collisionVector.Y;

                            //edge cases
                            if (Math.Abs(x) == 1 & Math.Abs(y) == 1) { return; }
                            // setting object velocity to zero, THIS WORKS
                            if (x == 1) { collisionDirection = "Left/West"; obj.vectorDir.X = 0; }
                            if (x == -1) { collisionDirection = "Right,East"; obj.vectorDir.X = 0; }
                            if (y == 1) { collisionDirection = "Up,North"; obj.vectorDir.Y = 0; }
                            if (y == -1) { collisionDirection = "Down,South"; obj.vectorDir.Y = 0; }

                        






                        }
                    }
                }
                foreach (var bounds in MapBoundries)
                {
                    if (obj.pos.Intersects(bounds) & obj.hasCollision)
                    {
                    }
                }
            }
        }
        public void boundsCheck()
        {
            //if (player.vectoPos.X) { } //left screen
            //if (player.vectoPos.X) { } //right screen
            //if (player.vectoPos.Y) { } //north
            // if (player.vectoPos.Y) { } //south
        }

        public bool personalCollisionCheck(GameObject @object)
        {
            foreach (var item in GameObjects)
            {
                if (@object.pos.Intersects(item.pos) & @object != item) { return true; }
            }
            return false;
        }


    }
}

