using Microsoft.Xna.Framework.Graphics;

namespace PirateGame
{
    /// <summary>
    /// Class that keeps track of animations and their sprites name and fps
    /// Made by: Julius
    /// </summary>
    public class Animation
    {
        public float FPS { get; private set; }
        public string Name { get; private set; }
        public Texture2D[] Sprites { get; private set; }
        public bool IsLooping { get; private set; }
        
        /// <summary>
        /// Contructor of the animation used to set varibles
        /// </summary>
        /// <param name="sprites">images of the animation</param>
        /// <param name="name">name of animation</param>
        /// <param name="fps">speed of animation</param>
        /// <param name="isLooping">checks if it a looping animation</param>
        public Animation(Texture2D[] sprites, string name, float fps, bool isLooping)
        {
            Sprites = sprites;
            Name = name;
            FPS = fps;
            IsLooping = isLooping;
           
        }
    }
}
