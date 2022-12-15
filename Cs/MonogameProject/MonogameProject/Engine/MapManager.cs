using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using MonoGame.Extended.Content;
using System.Net.Mime;
using TiledCS;

namespace MonoGameProject
{
    public class MapManager : Game
    {
        public MapManager(GraphicsDevice graphicsDevice)
        {
            Content.RootDirectory = "Content";
            Game1._currentMap = new TiledMap("C:\\Users\\hyman\\REPOS\\Cs\\MonogameProject\\MonoGameProject\\Content\\Maps\\simple.tmx");// tmx dont need built
            Game1._tileset = new TiledTileset("C:\\Users\\hyman\\REPOS\\Cs\\MonogameProject\\MonoGameProject\\Content\\Maps\\Oracle.tsx");
            Game1._tilesetTexture = Texture2D.FromFile(graphicsDevice,"C:\\Users\\hyman\\REPOS\\Cs\\MonogameProject\\MonoGameProject\\Content\\Maps\\oracle_tileset.png");
            Game1._tilesetTexture = Content.Load<Texture2D>("Maps//oracle_tileset");
             Game1._tiledRenderer = new tiledRenderer(graphicsDevice, Game1._spriteBatch, Game1._currentMap, Game1._tileset, Game1._tilesetTexture);
        }
        public void Update()
        {
            Game1._currentMap = new TiledMap("C:\\Users\\hyman\\REPOS\\Cs\\Monogametest\\Monogametest\\Content\\simple2.tmx");
        }
    }
}