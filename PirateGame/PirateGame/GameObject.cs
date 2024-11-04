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
    /// "GameObject" is a superclass for all object to inherit from
    /// </summary>
    abstract class GameObject
    {
        // Field
        protected Texture2D sprite;
        protected Texture2D[] sprites;
        protected Vector2 velocity;
        protected Vector2 position;
        protected Vector2 origin;
        protected float speed;
        protected float fps; // the animation speed
        private float timeElapsed; // time passed since frame changed
        private int currentIndex; // Index of current frame

        // Properties
        public Rectangle collisionBox
        {
            get
            {
                // note : origin gets defined in "Draw"
                return new Rectangle((int)position.X - (int)origin.X, (int)position.Y - (int)origin.Y, sprite.Width, sprite.Height);
            }
        }

        // Methods
        
        public abstract void LoadContent(ContentManager content);
        public abstract void Update(GameTime gameTime);
       
        /// <summary>
        /// "Draw" draws the sprite.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw (SpriteBatch spriteBatch)
        {       
            origin = new Vector2(sprite.Width / 2, sprite.Height / 2);
            spriteBatch.Draw(sprite, position, null, Color.White, 0, origin, 1, SpriteEffects.None, 1);
        }

        /// <summary>
        /// "Move" calculates the objects movement using gameTime, 
        /// velocity and speed to find its new position.
        /// </summary>
        /// <param name="gameTime"></param>
        protected void Move (GameTime gameTime)
        {
            // Calculate deltaTime based on the gameTime
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            //Move the object
            position += ((velocity * speed) * deltaTime);
        }

        /// <summary>
        /// "Animation" calculates how fast the sprites of an object 
        /// changes to create its animation.
        /// </summary>
        /// <param name="gameTime"></param>
        protected void Animation (GameTime gameTime)
        {
            //add time that has passed since last update
            timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;

            // calculate the currrnet index
            currentIndex = (int)(timeElapsed * fps);
            sprite = sprites[currentIndex];

            //check if the animation needs to restart
            if(currentIndex >= sprites.Length - 1)
            {
                //reset the animation
                timeElapsed = 0;
                currentIndex = 0;
            }
        }

        /// <summary>
        /// "OnCollision" tells the program what happens when two specified 
        /// objects collied.
        /// </summary>
        /// <param name="other"></param>
        public virtual void OnCollision(GameObject other)
        {

        }

        /// <summary>
        /// "CheckCollsion" checks if there has been a collsion between objects.
        /// If a collision is found it will call on the "OnCollision" method.
        /// </summary>
        /// <param name="other"></param>
        public void CheckCollision (GameObject other)
        {
            if(collisionBox.Intersects(other.collisionBox)&& other != this)
            {
                OnCollision(other);
            }
        }
        
        /// <summary>
        /// "TakeDamage" is called when an object with health
        /// collied with somthing that is suppose to damage it.
        /// </summary>
        public void TakeDamage()
        {

        }
    }
}
