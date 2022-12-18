using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Nez;
using Nez.Particles;
using Nez.Sprites;
using Nez.Textures;



namespace MonogameNezTest
{
    public class Game1 : Nez.Core
    {
        public Game1() : base()
        {

        }

        protected override void Initialize()
        {
            base.Initialize();
            //----------------------------------------------------------make scene first          
            Scene testScene = Scene.CreateWithDefaultRenderer(Color.CornflowerBlue);
            Core.Scene = testScene;
            //----------------------------------------------------------------- create a texture, create new entity
            Texture2D linkPic = testScene.Content.Load<Texture2D>("sprite");
           // Entity testEntity = Scene.CreateEntity("first_guy");
            //------------------------------------------------------------------- add a sprtie component to entity
            //testEntity.AddComponent(new SpriteRenderer(linkPic));
            //---------------------------------------------------------------------move (transform) entitygive new position
           // testEntity.Transform.SetPosition(new Vector2(300,300));


            //create map and renderer, entities draws automatically.
            var map = Content.LoadTiledMap("Content/easymap.tmx");
            var tiledEntity = Scene.CreateEntity("tiled-map");




























        }
    }
}