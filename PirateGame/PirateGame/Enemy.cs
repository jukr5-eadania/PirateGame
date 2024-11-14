using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1;
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
        // Field //     
        private int health;
        private bool goLeft = true; // For when the enemy needs to go left
        private bool pause = true; // For when the enemy needs to pause
        private bool patrol = true; // For when switching between patroling and attacking
        private float timeElaps;
        private int attackDamage;
        private bool isAttacking = false;
        private float cooldown; // The cooldown between its attack
        private bool isDead = false;
        private float pauseDeath; // The pause between when it dies and when it disappear (gets deleted)


        // Properties //


        // The collision box for when hitting the player / doing damage
        public override Rectangle attackBox
        {
            get
            {
                return new Rectangle((int)position.X + (int)origin.X, (int)position.Y - (int)origin.Y, 15, sprite.Height);
            }
        }

        // Methods //
        public Enemy()
        {
            health = 0;
            position = new Vector2(GameWorld.Width - 1000, (GameWorld.Height / 4) + 20);
            scale = 1.2f;

        }

        public override void LoadContent(ContentManager content)
        {

            Texture2D[] idle = new Texture2D[8]; // loading the animation for idle
            for (int i = 0; i < idle.Length; i++)
            {
                idle[i] = content.Load<Texture2D>($"Skeleton_White/Idle/Skeleton_White_{i}");
            }

            Texture2D[] walk = new Texture2D[10]; // loading the animation for walk
            for (int i = 0; i < walk.Length; i++)
            {
                walk[i] = content.Load<Texture2D>($"Skeleton_White/Walk/Skeleton_Walk_0{i}");
            }

            Texture2D[] attack = new Texture2D[10];// loading the animation for attack
            for (int i = 0; i < attack.Length; i++)
            {
                attack[i] = content.Load<Texture2D>($"Skeleton_White/Attack/Skeleton_Attack0{i}");
            }

            Texture2D[] hurt = new Texture2D[5];// loading the animation for hurt
            for (int i = 0; i < hurt.Length; i++)
            {
                hurt[i] = content.Load<Texture2D>($"Skeleton_White/Hurt/Skeleton_Hurt_{i}");
            }

            Texture2D[] die = new Texture2D[13];// loading the animation for die
            for (int i = 0; i < die.Length; i++)
            {
                die[i] = content.Load<Texture2D>($"Skeleton_White/Die/Skeleton_Die_{i}");
            }

            //creating the animation in the AddAnimation method in GameObject (class)
            //witch then adds them to the animation list
            AddAnimation(new Animation(idle, "skeleton_idle", 6, true));
            AddAnimation(new Animation(walk, "skeleton_walk", 6, true));
            AddAnimation(new Animation(attack, "skeleton_attack", 8, false));
            AddAnimation(new Animation(hurt, "skeleton_hurt", 6, false));
            AddAnimation(new Animation(die, "skeleton_die", 6, false));

            // Default animation
            PlayAnimation("skeleton_idle");

            sprite = currentAnimation.Sprites[0];
        }


        /// <summary>
        /// When player collides with the enemy's attackBox the enemy will attack
        /// </summary>
        /// <param name="other"></param>
        public override void OnCollision(GameObject other)
        {
            if (other is Player)
            {
                patrol = false;
                if (isAttacking == false)
                {
                    isAttacking = true;

                }

            }

        }

        /// <summary>
        /// When player hits or kills enemy
        /// </summary>
        public override void TakeDamage()
        {

            if (health <= 0)// when dead
            {
                isDead = true;
            }

        }

        /// <summary>
        /// When an animation is done playing
        /// </summary>
        /// <param name="name"></param>
        protected override void OnAnimationDone(string name)
        {
            if (name == "skeleton_die") //when it has played the death animation once
            {
                pauseAnimation = true;
                currentIndex = currentAnimation.Sprites.Length-1;
            }

        }
        /// <summary>
        /// When the enemy dies it plays its dead animation and then disappear and gets removed
        /// </summary>
        public void Dead()
        {
            if (isDead == true)
            {
                patrol = false;
                PlayAnimation("skeleton_die");

                if (pauseDeath >= 10f) // Note: see pauseDeath in Update
                {
                    GameWorld.RemoveObjects.Add(this);
                }
            }
        }

        /// <summary>
        /// When attacking the player
        /// </summary>
        public void Attack()
        {
            if (!isDead) // set to check if enemy is dead. if it is not dead contnue
            {
                attackDamage = 10;

                if (isAttacking)
                {
                    PlayAnimation("skeleton_attack");

                    if (cooldown >= 1f) // Note: see cooldown in Update
                    {
                        isAttacking = false;
                        cooldown = 0f;
                        patrol = true;
                    }

                }
            }


        }


        /// <summary>
        /// The enemy patrols a given path back and forth between two waypoints
        /// </summary>
        public void Patrol(GameTime gameTime)
        {

            this.speed = 2;
            int wp1 = 1000; // wp1 is the point towards the left
            int wp2 = 1350; // wp2 is the point towards the right

            if (patrol == true)
            {
                // The enemy pause for 1sec before contenuing to walk. 
                // This is done by checking if enemy pauses (if pause is true) 
                if (pause == true)
                {
                    PlayAnimation("skeleton_idle");
                    timeElaps += (float)gameTime.ElapsedGameTime.TotalSeconds;

                    if (timeElaps >= 1)
                    {
                        pause = false;
                        timeElaps = 0;
                    }
                }


                // After having paused pause will now be false in order to let it walk.
                // When enemy has reached its waypoint it will pause again (pause set to true)
                if (pause == false)
                {
                    // When the enemy walks left goLeft will be set to true
                    if (goLeft)
                    {
                        position.X -= speed; // goes left
                        PlayAnimation("skeleton_walk"); // runs the walk animation
                        spriteEffects = SpriteEffects.FlipHorizontally;

                        // When the enemy hits wp1 it will set goLeft to false so it now can 
                        // go right. Its new position.X will be set to wp1 as a precaution.
                        // Finally we set pause to true so it will stand stil for 1sec before continuing
                        if (position.X <= wp1)
                        {

                            goLeft = false;
                            position.X = wp1;
                            pause = true;
                        }

                    }
                    else // Here the enemy goes right after having gone left
                    {

                        position.X += speed; // goes right
                        PlayAnimation("skeleton_walk"); // run the walk animation
                        spriteEffects = SpriteEffects.None;

                        //Basically the same as in the goLeft part, but goLeft gets set to true,
                        // position.X is set to wp2 and once more pause will be set to true in order to 
                        // pause the enemy before continuing
                        if (position.X >= wp2)
                        {

                            goLeft = true;
                            position.X = wp2;
                            pause = true;
                        }

                    }

                }
            }

        }


        public override void Update(GameTime gameTime)
        {
            Animation(gameTime);
            Patrol(gameTime);
            Move(gameTime);
            Attack();
            TakeDamage();
            Dead();

            // cooldown timer for when attacking
            if (isAttacking)
            {
                cooldown += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

            // the timer for when playing the death animation before being deleted
            if (isDead == true)
            {
                pauseDeath += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }

        }

    }
}
