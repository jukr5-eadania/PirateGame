using Microsoft.Xna.Framework.Graphics;

namespace PirateGame
{
    internal class Animation
    {
        public float FPS { get; private set; }
        public string Name { get; private set; }

        public Texture2D[] Sprites { get; private set; }

        public Animation(Texture2D[] sprites, string name, float fps)
        {
            Sprites = sprites;
            Name = name;
            FPS = fps;
        }
    }
}
