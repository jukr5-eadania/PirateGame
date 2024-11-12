using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace PirateGame
{
    internal class Coin : GameObject
    {
        Random rnd = new Random();

        public Coin(Vector2 position)
        {
            
        }

        public override void LoadContent(ContentManager content)
        {
            Texture2D[] treasure = new Texture2D[6];

            for (int i = 0; i < treasure.Length; i++)
            {
                treasure[i] = content.Load<Texture2D>($"Treasure/treasure{i}");
            }

            sprite = treasure[rnd.Next(0,7)];
        }

        public override void Update(GameTime gameTime)
        {
            
        }

        public override void OnCollision(GameObject other)
        {
            base.OnCollision(other);
        }
    }
}
