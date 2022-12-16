using System;
using System.Diagnostics;
using System.IO;
using System.Reflection.Metadata;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TiledCS;
using Microsoft.Xna.Framework.Content;
using System.Security.Cryptography.X509Certificates;
using System.Runtime.CompilerServices;
using tile_test;

namespace Monogametest
{
    public class TiledMapRenderer
    {
        public TiledTileset _tileset;
        public Texture2D _tilesetTexture;
        public TiledMap _currentMap;
        public int _tileWidth;
        public int _tileHeight;
        public int _tilesetTilesWide;
        public int _tilesetTilesHeight;
        public TiledMapRenderer(GraphicsDevice graphicsDevice, TiledMap currentMap, TiledTileset tileset, Texture2D tilesetTexture)
        {
            _currentMap = currentMap;
            _tilesetTexture = tilesetTexture;
            _tileset = tileset;

            _tileWidth = tileset.TileWidth;
            _tileHeight = tileset.TileHeight;
            _tilesetTilesWide = tileset.Columns;
            _tilesetTilesHeight = tileset.TileCount / tileset.Columns;
        }

        public void Update(TiledMap currentMap, TiledTileset tileset, Texture2D tilesetTexture)
        {
            _currentMap = currentMap;
            _tilesetTexture = tilesetTexture;
            _tileset = tileset;

            _tileWidth = tileset.TileWidth;
            _tileHeight = tileset.TileHeight;
            _tilesetTilesWide = tileset.Columns;
            _tilesetTilesHeight = tileset.TileCount / tileset.Columns;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int l = 0; l < _currentMap.Layers.Length; l++)
            {
                for (var y = 0; y < _currentMap.Layers[l].height; y++)
                {
                    for (var x = 0; x < _currentMap.Layers[l].width; x++)
                    {
                        var index = y * _currentMap.Layers[l].width + x; // Assuming the default render order is used which is from right to bottom
                        var gid = _currentMap.Layers[l].data[index]; // The currentTileset tile index
                        var tileX = x * _currentMap.TileWidth;
                        var tileY = y * _currentMap.TileHeight;

                        //var testTile = new Tile();

                        // Gid 0 is used to tell there is no tile set
                        if (gid == 0)
                        {
                            continue;
                        }

                        var mapTileset = _currentMap.GetTiledMapTileset(gid);
                        var rect = _currentMap.GetSourceRect(mapTileset, _tileset, gid);

                        var source = new Rectangle(rect.x, rect.y, rect.width, rect.height);
                        var destination = new Rectangle(tileX, tileY, _currentMap.TileWidth, _currentMap.TileHeight);

                        //render
                        spriteBatch.Draw(_tilesetTexture, destination, source, Color.White);
                    }
                }
            }

        }
        /*
        public void altDraw(SpriteBatch spriteBatch)
        {
            for (var j = 0; j < _currentMap.Layers.Length; j++)
            {
                for (var i = 0; i < _currentMap.Layers[j].data.Length; i++)
                {
                    int gid = _currentMap.Layers[j].data[i];
                    int tileFrame = gid - 1;

                    if (gid == 0)
                    {
                        continue;
                    }

                    var tile = _currentMap.GetTiledTile(_currentMap.Tilesets[0], _tileset, gid);

                    int column = tileFrame % _tilesetTilesWide;
                    int row = (int)Math.Floor((double)tileFrame / (double)_tilesetTilesWide);

                    float x = (i % _currentMap.Width) * _currentMap.TileWidth;
                    float y = (float)Math.Floor(i / (double)_currentMap.Width) * _currentMap.TileHeight;

                    Rectangle tilesetRec = new Rectangle(_tileWidth * column, _tileHeight * row, _tileWidth, _tileHeight);

                    spriteBatch.Draw( _tilesetTexture, new Rectangle((int)x, (int)y, _tileWidth, _tileHeight), tilesetRec, Color.White);

                }
            }
        }
        */
    }

}
