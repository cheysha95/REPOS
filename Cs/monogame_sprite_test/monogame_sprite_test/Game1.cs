using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Spritesheet;
using System.Collections.Generic;
using monogame_sprite_test;
using System;

namespace monogame_sprite_test
{
    public class Game1 : Game
    {
        public static GraphicsDeviceManager _graphics;
        public static SpriteBatch _spriteBatch;
        public static Texture2D _spriteSheet;
        public static Dictionary <string, AnimatedSprite> animations;
        public static states current_state;
        public enum states
        {
            idleU,
            idleD,
            idleL,
            idleR,
            walkingU,
            walkingD,
            walkingL,
            walkingR
        }


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
  
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
          
            _spriteSheet = Content.Load<Texture2D>("link_spriteSheet");
            
            current_state = states.walkingD;

            animations = new Dictionary<string, AnimatedSprite>();
            //----------------------------idle
            animations.Add("idleR", new AnimatedSprite(_spriteSheet, 0, 0));
            animations.Add("idleL", new AnimatedSprite(_spriteSheet, 4, 4));
            animations.Add("idleU", new AnimatedSprite(_spriteSheet, 2, 2));
            animations.Add("idleD", new AnimatedSprite(_spriteSheet, 8, 8));
            //---------------------------walking
            animations.Add("walkingR" , new AnimatedSprite(_spriteSheet, 0, 1));
            animations.Add("walkingL", new AnimatedSprite(_spriteSheet, 4, 5));
            animations.Add("walkingU", new AnimatedSprite(_spriteSheet, 2, 3));
            animations.Add("walkingD", new AnimatedSprite(_spriteSheet, 6, 7));
        }

        protected override void Update(GameTime gameTime)
        {

            animations[current_state.ToString()].Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            GraphicsDevice.Clear(Color.CornflowerBlue);

            animations[current_state.ToString()].Draw(new Vector2(32, 0));

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}