using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1.Effects;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PirateGame
{
    /// <summary>
    /// Background class that creats the background
    /// Made by: Julius
    /// </summary>
    internal class Background
    {
        private Texture2D sprite;
        private Vector2 position;
        private Vector2 origin;

        /// <summary>
        /// Adds the sprite/image
        /// </summary>
        /// <param name="content"></param>
        public void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("Background");
        }

        /// <summary>
        /// Draws the sprites and edit its origing
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(SpriteBatch spriteBatch)
        {
            origin = new Vector2(sprite.Width / 2, sprite.Height / 2);

            for (int i = 0; i < 10; i++)
            {
                position = new Vector2(-sprite.Width + sprite.Width * i, 355);
                spriteBatch.Draw(sprite, position, null, Color.White, 0, origin, 1, SpriteEffects.None, 0);
            }
        }
    }
}
