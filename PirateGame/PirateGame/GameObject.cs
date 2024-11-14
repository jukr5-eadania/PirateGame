using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace PirateGame
{
    /// <summary>
    /// "GameObject" is a superclass for all object to inherit from
    /// </summary>
    public abstract class GameObject
    {
        // Field //
        protected Texture2D sprite;
        protected Vector2 velocity;
        protected Vector2 jumpVelocity;
        protected Vector2 position;
        protected Vector2 origin;
        protected float speed;
        protected Animation currentAnimation;
        protected SpriteEffects spriteEffects = SpriteEffects.None;
        protected Dictionary<string, Animation> animations = new Dictionary<string, Animation>();
        protected int currentIndex; // Index of current frame
        protected float timeElapsed; // time passed since frame changed
        protected List<GameObject> collidingObjects = new List<GameObject>();
        protected float scale = 1;
        protected bool pauseAnimation = false;

        // Properties //
        public virtual Rectangle collisionBox
        {
            get
            {
                // note : origin gets defined in "Draw"
                return new Rectangle((int)Position.X - (int)origin.X, (int)Position.Y - (int)origin.Y, sprite.Width, sprite.Height);
            }
        }

        public Vector2 Position { get => position; set => position = value; }

        // Methods

        public virtual Rectangle attackBox { get; }
        

        // Methods //
        public abstract void LoadContent(ContentManager content);
        public abstract void Update(GameTime gameTime);

        /// <summary>
        /// "Draw" draws the sprite.
        /// </summary>
        /// <param name="spriteBatch"></param>
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            origin = new Vector2(sprite.Width / 2, sprite.Height / 2);
            spriteBatch.Draw(sprite, position, null, Color.White, 0, origin, scale, spriteEffects, 1);
        }

        /// <summary>
        /// "Move" calculates the objects movement using gameTime, 
        /// velocity and speed to find its new position.
        /// </summary>
        /// <param name="gameTime"></param>
        protected void Move(GameTime gameTime)
        {
            // Calculate deltaTime based on the gameTime
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            //Move the object
            Position += ((velocity * speed) * deltaTime);

            Position += ((jumpVelocity * speed) * deltaTime);
        }

        /// <summary>
        ///  When the animation is done call this method to tell it what it should do next (i.e. go back to idle)
        /// </summary>
        /// <param name="name"></param>
        protected virtual void OnAnimationDone(string name) { }

        /// <summary>
        /// "Animation" calculates how fast the sprites of an object 
        /// changes to create its animation.
        /// </summary>
        /// <param name="gameTime"></param>
        protected virtual void Animation (GameTime gameTime)
        {
            if (!pauseAnimation)
            {
                //add time that has passed since last update
                timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;

                // calculate the currrnet index
                currentIndex = (int)(timeElapsed * currentAnimation.FPS);

                //check if the animation needs to restart
                if (currentIndex >= currentAnimation.Sprites.Length)
                {
                    //reset the animation
                    timeElapsed = 0;
                    currentIndex = 0;
                    OnAnimationDone(currentAnimation.Name);

                }  
                // if the animation is only run once (pauseAnimation = true), the animation will stop on its last frame
                else if (currentIndex >= currentAnimation.Sprites.Length && !currentAnimation.IsLooping)
                {

                    pauseAnimation = true;
                    return;
                }

                sprite = currentAnimation.Sprites[currentIndex];
            }

        }

        /// <summary>
        /// Plays an animation and makes sure the animation doesn't create an array overflow
        /// </summary>
        /// <param name="animationName">The name of the animation that is about to be played</param>
        public void PlayAnimation(string animationName)
        {
            if (animationName != currentAnimation.Name)
            {
                currentAnimation = animations[animationName];
                timeElapsed = 0;
                currentIndex = 0;
                pauseAnimation = false;
            }
        }

        /// <summary>
        /// Adds animations to the animation dictionary
        /// </summary>
        /// <param name="animation">Takes an animation from the Animation class</param>
        public void AddAnimation(Animation animation)
        {
            animations.Add(animation.Name, animation);

            if (currentAnimation == null)
            {
                currentAnimation = animation;
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

        public virtual void OnCollisionExit(GameObject other)
        {

        }

        public virtual void OnCollisionEnter(GameObject other)
        {

        }

        /// <summary>
        /// "CheckCollsion" checks if there has been a collsion between objects.
        /// If a collision is found it will call on the "OnCollision" method.
        /// </summary>
        /// <param name="other"></param>
        public void CheckCollision(GameObject other)
        {
            if (collisionBox.Intersects(other.collisionBox) && other != this && !collidingObjects.Contains(other))
            {
                OnCollisionEnter(other);
                collidingObjects.Add(other);
            }
            else if (collisionBox.Intersects(other.collisionBox) && other != this)
            {
                OnCollision(other);
            }
            else if (collidingObjects.Contains(other))
            {
                OnCollisionExit(other);
                collidingObjects.Remove(other);
            }

            if (attackBox.Intersects(other.collisionBox) && other != this)
            {
                OnCollision(other);
            }
        }

        /// <summary>
        /// "TakeDamage" is called when an object with health
        /// collied with somthing that is suppose to damage it.
        /// </summary>
        public virtual void TakeDamage()
        {

        }
    }
}
