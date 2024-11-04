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
        public Rectangle collisionBox { get; }

        // Methods
        public abstract void LoadContent(ContentManager content);
        public abstract void Update(GameTime gameTime);
       
        public void Draw (SpriteBatch spriteBatch)
        {
            origin = new Vector2(sprite.Width / 2, sprite.Height / 2);
            spriteBatch.Draw(sprite, position, null, Color.White, 0, origin, 1, SpriteEffects.None, 1);
        }
        protected void Move (GameTime gameTime)
        {
            // Calculate deltaTime based on the gameTime
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            //Move the object
            position += ((velocity * speed) * deltaTime);
        }
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
        public virtual void OnCollision(GameObject other)
        {

        }
        public void CheckCollision (GameObject other)
        {
            if(collisionBox.Intersects(other.collisionBox)&& other != this)
            {
                OnCollision(other);
            }
        }
        public void TakeDamage()
        {

        }
    }
}
