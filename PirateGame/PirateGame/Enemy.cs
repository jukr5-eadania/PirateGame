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
    internal class Enemy : GameObject
    {
        // Field        
        private int health;        
        private Dictionary<string, Texture2D[]> spritesAnimation = new Dictionary<string, Texture2D[]>();

        // Properties

        // Methods
        public Enemy()
        {
            health = 50;
            fps = 12;
            position = new Vector2(GameWorld.Width / 2, GameWorld.Height / 2);
            scale = 2;
            
        }

        public override void LoadContent(ContentManager content)
        {
                        
            // make the idle animation
            sprites = new Texture2D[8];
            for(int i = 0; i < sprites.Length; i++)
            {
                sprites[i] = content.Load<Texture2D>($"Skeleton_White/Idle/Skeleton_White_{i}");
            }
            // set a default sprite
            sprite = sprites[0];

           
        }

        /// <summary>
        /// When colliding with player
        /// </summary>
        /// <param name="other"></param>
        public override void OnCollision(GameObject other)
        {

        }

        /// <summary>
        /// When player or bullet hits enemy
        /// </summary>
        public override void TakeDamage()
        {

        }

        public void Patrol()
        {
            this.speed = 5;
                                

           if (position.X <= 400) //as long as position.x is less then 400: go right
           {
                velocity += new Vector2(+1, 0);
           }
           if (position.X >= 650) 
           {
                velocity += new Vector2(-1, 0);
           }

        }
                

        public override void Update(GameTime gameTime)
        {
            Animation(gameTime);
            Patrol();
            Move(gameTime);
        }

    }  
}
