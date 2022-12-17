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
    public class MapManager 
    {
        string projectPath = Environment.CurrentDirectory + "\\..\\..\\..\\"; //actual path is wonk, get a proper path for project
        List<TiledMap> MapList;  // list of all maps in directory
        public int mapTileWidth = 3; //mapgrids across, y 
        public TiledTileset currentTileset;
        public Texture2D currentTilesetTexture;
        public TiledMap currentMap;
        public int currentMapIndex;
        public int _tilesetTilesWide;
        public int _tilesetTilesHeight;
        public int _tileWidth;
        public int _tileHeight;
        //-----------------------------------------------------------
        public ContentManager content;
        List<Rectangle> MapBoundries;
        List<GameObject> GameObjects;

        public Player player;
        //-----------------------------------------------------------------

        public MapManager(GraphicsDevice graphicsDevice, ContentManager Content)
        {
            content = Content;
            loadFromDisk(graphicsDevice);

            //THESE NEED TO BE SOUTH HERE
            currentMapIndex = 4; //set spawn room
            currentMap = MapList[currentMapIndex];
            //--------------------------------------------------------------------

            player = new Player(content, new Vector2(10, 10), 255); //DEBUG PLAYER

            ListRebuild(currentMap);
        }
        public void Update(GameTime gameTime)
        {
  

            UpdateObjects(gameTime);

            CollisionCheck();

            screenBoundyCheck();

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            MapDraw(spriteBatch);

            DrawObjects(spriteBatch);
        }

        public void loadFromDisk(GraphicsDevice graphicsDevice)
        {
            MapList = new List<TiledMap>();

            string mapsPath = projectPath + "Content\\Maps\\";
            currentTileset = new TiledTileset(mapsPath + "Oracle_tileset.tsx");
            currentTilesetTexture = Texture2D.FromFile(graphicsDevice, mapsPath + "Oracle_Tileset.png");
            DirectoryInfo d = new DirectoryInfo(mapsPath);
            foreach (var map in d.GetFiles("*.tmx"))
            {
                MapList.Add(new TiledMap(map.FullName)); //adds them to list, 
            }

        }
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

        public void shiftMap(Direction direction)
        {
            if (direction == Direction.NORTH)
            {
                currentMapIndex = currentMapIndex - mapTileWidth;
                currentMap = MapList[currentMapIndex];
                ListRebuild(currentMap);
            }
            if (direction == Direction.SOUTH)
            {
                currentMapIndex = currentMapIndex + mapTileWidth;
                currentMap = MapList[currentMapIndex];
                ListRebuild(currentMap);
            }
            if (direction == Direction.EAST)
            {
                currentMapIndex = currentMapIndex + 1;
                currentMap = MapList[currentMapIndex];
                ListRebuild(currentMap);
            }
            if (direction == Direction.WEST)
            {
                currentMapIndex = currentMapIndex - 1;
                currentMap = MapList[currentMapIndex];
                ListRebuild(currentMap);
            }
            reseatPlayer(direction);
        }
        public void ListRebuild(TiledMap CurrentMap)
        {
            MapBoundries = new List<Rectangle>();
            GameObjects = new List<GameObject>();
            GameObjects.Add(player);
            loadObjectsFromMap(CurrentMap);
            void loadObjectsFromMap(TiledMap CurrentMap)
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
        }

        public void reseatPlayer(Direction direction)
        {
            // celled by shift map when shifting map
            if (direction == Direction.NORTH) { player.vectorPos.Y = currentMap.Height * currentMap.TileHeight - player.pos.Height ; }
            if (direction == Direction.SOUTH) { player.vectorPos.Y = 0; }
            if (direction == Direction.WEST) { player.vectorPos.X = currentMap.Width * currentMap.TileWidth - player.pos.Width; }
            if (direction == Direction.EAST) { player.vectorPos.X = 0; }
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
                    if (obj.pos.Intersects(objj.pos) & obj != objj) { obj.onCollide(objj);}
                } 
                foreach (var bounds in MapBoundries)
                {
                    if (obj.pos.Intersects(bounds) & obj.hasCollision) { obj.onCollide(bounds); }
                } 
            }
        }
        public void screenBoundyCheck()
        {
            if (player.vectorPos.X <= 0) { shiftMap(Direction.WEST); } //left screen
            if (player.pos.Right >= currentMap.Width * currentMap.TileWidth) { shiftMap(Direction.EAST); } //right screen
            if (player.pos.Top <= 0) { shiftMap(Direction.NORTH); } //north
            if (player.pos.Bottom >= currentMap.Height * currentMap.TileHeight) { shiftMap(Direction.SOUTH); } //south
        }

        //--------------------------------------------------------------------------

        public void MapDraw(SpriteBatch spriteBatch)
        {
            for (int l = 0; l < currentMap.Layers.Length; l++)
            {
                for (var y = 0; y < currentMap.Layers[l].height; y++)
                {
                    for (var x = 0; x < currentMap.Layers[l].width; x++)
                    {   
                        var index = y * currentMap.Layers[l].width + x; // Assuming the default render order is used which is from right to bottom
                        var gid = currentMap.Layers[l].data[index]; // The currentTileset tile index
                        var tileX = x * currentMap.TileWidth;
                        var tileY = y * currentMap.TileHeight;

                        // Gid 0 is used to tell there is no tile per tiled
                        if (gid == 0)
                        {
                            continue;
                        }

                        var mapTileset = currentMap.GetTiledMapTileset(gid);
                        var rect = currentMap.GetSourceRect(mapTileset, currentTileset, gid);

                        var source = new Rectangle(rect.x, rect.y, rect.width, rect.height);
                        var destination = new Rectangle(tileX, tileY, currentMap.TileWidth, currentMap.TileHeight);

                        //render
                        spriteBatch.Draw(currentTilesetTexture, destination, source, Color.White);
                    }
                }
            }

        }


    }
}

