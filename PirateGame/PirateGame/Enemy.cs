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
        private bool goLeft = true;
        private bool pause = true;
        private float timeElaps;

        // Properties

        // Methods
        public Enemy()
        {
            health = 50;            
            position = new Vector2(GameWorld.Width / 2, GameWorld.Height / 2);
            scale = 2;
            
        }

        public override void LoadContent(ContentManager content)
        {
                        
            
            Texture2D[] idle = new Texture2D[8];
            for(int i = 0; i < idle.Length; i++)
            {
                idle[i] = content.Load<Texture2D>($"Skeleton_White/Idle/Skeleton_White_{i}");
            }

            AddAnimation(new Animation(idle, "skeleton_idle", 12));


           
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
        /// <summary>
        /// The enemy patrols a given path back and forth
        /// </summary>
        public void Patrol(GameTime gameTime)
        {

            this.speed = 4;

            if (pause == true)
            {

                timeElaps += (float)gameTime.ElapsedGameTime.TotalSeconds;

                if (timeElaps >= 1)
                {
                    pause = false;
                    timeElaps = 0;
                }
            }
            if (pause == false)
            {
                if (goLeft)
                {
                    position.X -= speed;
                    if (position.X <= 600) // when hitting the max point (600) turn around and go left
                    {
                        goLeft = false;
                        position.X = 600;
                        pause = true;
                    }

                }
                else
                {
                    position.X += speed;
                    if (position.X >= 800) // when hitting the low(minimum) point (800) turn around and go right 
                    {
                        goLeft = true;
                        position.X = 800;
                        pause = true;
                    }

                }



            }
        }     

        public override void Update(GameTime gameTime)
        {
            Animation(gameTime);
            Patrol(gameTime);
            Move(gameTime);
        }

    }  
}
