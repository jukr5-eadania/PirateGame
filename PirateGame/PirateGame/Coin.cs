using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace PirateGame
{
    /// <summary>
    /// Coin class that keeps track of the coins.
    /// The class is a child of GameObject
    /// Made by: Julius
    /// </summary>
    internal class Coin : GameObject
    {
        Random rnd = new Random();

        /// <summary>
        /// the coin stats
        /// </summary>
        /// <param name="position">a Vector2D that sets position</param>
        public Coin(Vector2 position)
        {
            this.Position = position;
        }

        /// <summary>
        /// load the sprites and add the animations they need
        /// </summary>
        /// <param name="content"></param>
        public override void LoadContent(ContentManager content)
        {
            Texture2D[] treasure = new Texture2D[6];

            for (int i = 0; i < treasure.Length; i++)
            {
                treasure[i] = content.Load<Texture2D>($"Treasure/treasure{i}");
            }

            sprite = treasure[rnd.Next(0,6)];
        }

        /// <summary>
        /// The main loop of coins
        /// </summary>
        /// <param name="gameTime">Takes a GameTime that provides the timespan since last call to update</param>
        public override void Update(GameTime gameTime)
        {
            
        }

        /// <summary>
        /// "OnCollision" tells the program what happens when two specified 
        /// objects collied.
        /// </summary>
        /// <param name="other">name for the other gameobject that is collided with</param>
        public override void OnCollision(GameObject other)
        {
            base.OnCollision(other);
        }
    }
}
