using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace MonoGameProject
{
    public class Npc : GameObject
    {
        string text;

        public Npc(GraphicsDevice graphicsDeive, int x, int y) : base(graphicsDeive, x, y)
        {
            base._texture = Content.Load<Texture2D>("npc");
            text = "This is some debug text for a NPC";
            base.name = "npc";
        }

    }
}

