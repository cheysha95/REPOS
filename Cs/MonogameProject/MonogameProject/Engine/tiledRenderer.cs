using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TiledCS;
using Microsoft.Xna.Framework.Content;



namespace MonoGameProject
{
    public class tiledRenderer : Game
    {
        public TiledMap _map { get; set; }
        public TiledTileset _tileset { get; set; }

        public Texture2D _tilesetTexture;
        public int _tileWidth { get; set; }
        public int _tileHeight { get; set; }
        public int _tilesetTilesWide { get; set; }
        public int _tilesetTilesHeight { get; set; }
        public SpriteBatch _spriteBatch { get; set; }



        public tiledRenderer(GraphicsDevice graphicsDevice, SpriteBatch _spriteBatch, TiledMap _map, TiledTileset _tileset, Texture2D _tilesetTexture)
        {
            this._spriteBatch = _spriteBatch;
            this._map = _map;
            this._tileset = _tileset;
            this._tilesetTexture = _tilesetTexture;

            _tileWidth = _tileset.TileWidth;
            _tileHeight = _tileset.TileHeight;

            // Amount of tiles on each row (left right)
            _tilesetTilesWide = _tileset.Columns;

            // Amount of tiels on each column (up down)
            _tilesetTilesHeight = _tileset.TileCount / _tileset.Columns;
        }

        public void draw()
        {
            for (int l = 0; l < _map.Layers.Length; l++)
            {
                for (var y = 0; y < _map.Layers[l].height; y++)
                {
                    for (var x = 0; x < _map.Layers[l].width; x++)
                    {
                        var index = (y * _map.Layers[l].width) + x; // Assuming the default render order is used which is from right to bottom
                        var gid = _map.Layers[l].data[index]; // The tileset tile index
                        var tileX = (x * _map.TileWidth);
                        var tileY = (y * _map.TileHeight);

                        // Gid 0 is used to tell there is no tile set
                        if (gid == 0)
                        {
                            continue;
                        }

                        var mapTileset = _map.GetTiledMapTileset(gid);
                        var rect = _map.GetSourceRect(mapTileset, _tileset, gid);

                        var source = new Rectangle(rect.x, rect.y, rect.width, rect.height);
                        var destination = new Rectangle(tileX, tileY, _map.TileWidth, _map.TileHeight);

                        // Helper method to fetch the right TieldMapTileset instance. 
                        // This is a connection object Tiled uses for linking the correct tileset to the gid value using the firstgid property.


                        // Retrieve the actual tileset based on the firstgid property of the connection object we retrieved just now
                        // var tileset = _tileset[mapTileset.firstgid];

                        // Use the connection object as well as the tileset to figure out the source rectangle.



                        // Render sprite at position tileX, tileY using the rect

                        _spriteBatch.Draw(_tilesetTexture, destination, source, Color.White);
                    }
                }
            }

        }

        public void altDraw()
        {
            for (var j = 0; j < _map.Layers.Length; j++)
            {
                for (var i = 0; i < _map.Layers[j].data.Length; i++)
                {
                    int gid = _map.Layers[j].data[i];
                    int tileFrame = gid - 1;

                    if (gid == 0)
                    {
                        continue;
                    }

                    var tile = _map.GetTiledTile(_map.Tilesets[0], _tileset, gid);

                    int column = tileFrame % _tilesetTilesWide;
                    int row = (int)Math.Floor((double)tileFrame / (double)_tilesetTilesWide);

                    float x = (i % _map.Width) * _map.TileWidth;
                    float y = (float)Math.Floor(i / (double)_map.Width) * _map.TileHeight;

                    Rectangle tilesetRec = new Rectangle(_tileWidth * column, _tileHeight * row, _tileWidth, _tileHeight);

                    _spriteBatch.Draw(_tilesetTexture, new Rectangle((int)x, (int)y, _tileWidth, _tileHeight), tilesetRec, Color.White);

                }
            }
        }
    }

}
