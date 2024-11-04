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

        }
        protected void Move (GameObject other)
        {

        }
        public void OnCollision(GameObject other)
        {

        }
        public void CheckCollision (GameObject other)
        {

        }
        public void TakeDamage()
        {

        }
    }
}
