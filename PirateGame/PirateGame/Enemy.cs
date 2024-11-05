using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PirateGame
{
    /// <summary>
    /// "Enemy" is a sup-class from GameObject.
    /// </summary>
    abstract class Enemy : GameObject
    {
        // Field        
        private int health;
        private Dictionary<string, Texture2D> sprites;

        // Properties

        // Methods
        public override void LoadContent(ContentManager content)
        {
            sprites = new Dictionary<string, Texture2D>();
            // Skeleton sprites for the idle animation added to the dictionary under the same keyword
            sprites.Add("Idle", content.Load<Texture2D>("Skeleton_White_0"));
            sprites.Add("Idle", content.Load<Texture2D>("Skeleton_White_1"));
            sprites.Add("Idle", content.Load<Texture2D>("Skeleton_White_2"));
            sprites.Add("Idle", content.Load<Texture2D>("Skeleton_White_3"));
            sprites.Add("Idle", content.Load<Texture2D>("Skeleton_White_4"));
            sprites.Add("Idle", content.Load<Texture2D>("Skeleton_White_5"));
            sprites.Add("Idle", content.Load<Texture2D>("Skeleton_White_6"));
            sprites.Add("Idle", content.Load<Texture2D>("Skeleton_White_7"));



            // make the idle animation
            /*sprites = new Texture2D[7];
            for(int i = 0; i < sprites.Length; i++)
            {
                sprites[i] = content.Load<Texture2D>($"Skeleton_White_{i}");
            }
            // set a default sprite
            sprite = sprites[0];*/
        }

        /// <summary>
        /// When colliding with player
        /// </summary>
        /// <param name="other"></param>
        public void OnCollision (GameObject other)
        {
            
        }

        /// <summary>
        /// When player or bullet hits enemy
        /// </summary>
        public void TakeDamage()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }

        public void Patrol()
        {

        }

        public override void Update(GameTime gameTime)
        {
            
        }


    }
}
