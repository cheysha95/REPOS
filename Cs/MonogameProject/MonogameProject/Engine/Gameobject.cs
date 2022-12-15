using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;

namespace MonoGameProject
{
    public class GameObject : Game
    {


        public int x, y;
        public Vector2 vpos;
        public int tilex, tiley;
        public int xspeed, yspeed;
        public Rectangle pos;
        public Texture2D _texture;
        public int hp;
        public int moveSpeed;
        //public List<gameObject> objectList;
        int objectWidth, objectHeight;
        public string name;

        public GameObject(GraphicsDevice graphicsDevice, int x, int y)
        {
           // GraphicsDevice graphics = graphicsDevice;
            Content.RootDirectory = "Content";
            objectWidth = 16;
            objectHeight = 16;
            this.vpos.X = x;
            this.vpos.Y = y;
            //_texture = Content.Load<Texture2D>("SpriteSheets//link"); // placeholder
            this.xspeed = 0;
            this.yspeed = 0;
            this.pos = new Rectangle(x, y, objectWidth, objectHeight);
            this.hp = 10;
            this.moveSpeed = 2;
           // objectList = Game1.objectList;
            tilex = x / Game1._currentMap.TileWidth;
            tiley = y / Game1._currentMap.TileHeight;
            name = "";
        }
    }
}