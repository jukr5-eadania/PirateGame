using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace PirateGame
{
    /// <summary>
    /// "GameObject" is a superclass that all object will inherit from
    /// Made by: Julius, Emilie, Mads
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
        public virtual Rectangle attackBox { get; }

        
        // Methods //

        /// <summary>
        /// This method is abstract so that all objects can override it 
        /// and load the sprites and add the animations they need
        /// </summary>
        /// <param name="content"></param>
        public abstract void LoadContent(ContentManager content);

        /// <summary>
        /// The main loop of the objcet
        /// </summary>
        /// <param name="gameTime">Takes a GameTime that provides the timespan since last call to update</param>
        public abstract void Update(GameTime gameTime);

        /// <summary>
        /// Draws the sprites and edit its origing
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
        ///  When the animation is done call this method to tell it what it should do next (i.e. go back to idle).
        ///  This method is virtual so only the objects that use this will have to override it.
        /// </summary>
        /// <param name="name"> name is the key value of the animation dictionary </param>
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
        /// Plays a new animation
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
        /// <param name="other">name for the other gameobject that is collided with</param>
        public virtual void OnCollision(GameObject other)
        {

        }

        /// <summary>
        /// When exiting a collision this is used
        /// </summary>
        /// <param name="other">name for the other gameobject that is collided with</param>
        public virtual void OnCollisionExit(GameObject other)
        {
            
        }

        /// <summary>
        /// When entering an collision this is used
        /// </summary>
        /// <param name="other">name for the other gameobject that is collided with</param>
        public virtual void OnCollisionEnter(GameObject other)
        {

        }

        /// <summary>
        /// "CheckCollsion" checks if there has been a collsion between objects.
        /// If a collision is found it will call on the "OnCollision" method.
        /// </summary>
        /// <param name="other">name for the other gameobject that is collided with</param>
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

    }
}
