using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection.Metadata;
using System.Runtime.Serialization;
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
        public enum MAPMGRSTATES {IDLE,NORTH,SOUTH,WEST,EAST}
        MGRSTATES CURRENTMGRSTATE;
        MAPMGRSTATES CURRENTMAPMGRSTATE;
        bool isRunningObjectUpdate = false;

    
        string projectPath = Environment.CurrentDirectory + "\\..\\..\\..\\"; //actual path is wonk, get a proper path for project
        List<TiledMap> MapList = new List<TiledMap>();    // list of all maps in directory
        public int mapTileWidth = 3; //mapgrids across, y 
        public TiledTileset currentTileset;
        public Texture2D currentTilesetTexture;
        public TiledMap currentMap;
        public int currentMapIndex;
        public TiledMapRenderer tileRenderer;
        public ObjectManager _objectManager;
       // public GameTime gameTime;

        public MapManager(GraphicsDevice graphicsDevice, ContentManager contentManager)
        {
            initLoad(graphicsDevice);

            //THESE NEED TO BE SOUTH HERE
            currentMapIndex = 4;        // set spawn room
            currentMap = MapList[currentMapIndex];

            _objectManager = new ObjectManager(contentManager,currentMap);
            tileRenderer = new TiledMapRenderer(graphicsDevice, currentMap, currentTileset, currentTilesetTexture);

            changeState(MGRSTATES.IDLE);
        }

        // Updates Map
        public void Update(GameTime gameTime)
        {
            changeState(MGRSTATES.UPDATING);

            ChangeMap();

            changeState(MGRSTATES.IDLE);
        }

        //Updates objects in map, 2nd update method, runs right after update
        public void ObjectUpdate(GameTime gameTime)// objects get own update method so mapmgr state actually means somthing
        {
            changeState(MGRSTATES.UPDATING);
            isRunningObjectUpdate= true;

            _objectManager.Update(gameTime);

            changeState(MGRSTATES.IDLE);
            isRunningObjectUpdate= false;
        }




        // Loads new map and calls object managers room rebuild method.....maight need to go to map mgr
        public void ChangeMap() // UpdateMap currentmap, object list, collisionlsit, can add to UpdateMap currentTileset
        {
    
      
            if (CURRENTMAPMGRSTATE == MAPMGRSTATES.NORTH)
            {
                currentMapIndex = currentMapIndex - mapTileWidth;
                currentMap = MapList[currentMapIndex];
                _objectManager.RoomRebuild(currentMap);
                tileRenderer.UpdateMap(currentMap, currentTileset, currentTilesetTexture);
                resetMapMgrState();
            }
            if (CURRENTMAPMGRSTATE == MAPMGRSTATES.SOUTH)
            {
                currentMapIndex = currentMapIndex + mapTileWidth;
                currentMap = MapList[currentMapIndex];
                _objectManager.RoomRebuild(currentMap);
                tileRenderer.UpdateMap(currentMap, currentTileset, currentTilesetTexture);
                resetMapMgrState();
            }
            if (CURRENTMAPMGRSTATE == MAPMGRSTATES.EAST)
            {
                currentMapIndex = currentMapIndex + 1;
                currentMap = MapList[currentMapIndex];
                _objectManager.RoomRebuild(currentMap);
                tileRenderer.UpdateMap(currentMap, currentTileset, currentTilesetTexture);
                resetMapMgrState();
            }
            if (CURRENTMAPMGRSTATE == MAPMGRSTATES.WEST)
            {
                currentMapIndex = currentMapIndex - 1;
                currentMap = MapList[currentMapIndex];
                _objectManager.RoomRebuild(currentMap);
                tileRenderer.UpdateMap(currentMap, currentTileset, currentTilesetTexture);
                resetMapMgrState();
            }
        }


        // i think theres some mess here, i dont like 


        //Draws the maprenderer and object list draw
        public void Draw(SpriteBatch spriteBatch)
        {
            changeState(MGRSTATES.DRAWING);


            tileRenderer.Draw(spriteBatch);
            _objectManager.Draw(spriteBatch);

            changeState(MGRSTATES.IDLE);
        } 


        //loads maps from disk
        public void initLoad(GraphicsDevice graphicsDevice)
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


        public void resetMapMgrState()
        {
            CURRENTMAPMGRSTATE = MAPMGRSTATES.IDLE;
        }
        public void changeState(MGRSTATES State)
        {
            CURRENTMGRSTATE = State;
        }





    }

}

