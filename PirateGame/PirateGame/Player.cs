using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace PirateGame
{
    internal class Player : GameObject
    {
        private int health;
        private int damage;
        private float gravity = 10;
        private bool onGround = true;

        public Player()
        {
            speed = 200;
        }

        public override void LoadContent(ContentManager content)
        {
            position = new Vector2(GameWorld.Width- 200, GameWorld.Height / 2);

            Texture2D[] idle = new Texture2D[5];

            for (int i = 0; i < idle.Length; i++)
            {
                idle[i] = content.Load<Texture2D>($"Pirate/Idle/idle{i}");
            }

            AddAnimation(new Animation(idle, "idle", 10));

            Texture2D[] run = new Texture2D[6];

            for (int i = 0; i < run.Length; i++)
            {
                run[i] = content.Load<Texture2D>($"Pirate/Run/run{i}");
            }

            AddAnimation(new Animation(run, "run", 10));

            PlayAnimation("idle");
        }

        public override void Update(GameTime gameTime)
        {
            HandleInput();
            Move(gameTime);
            Animation(gameTime);

            if (velocity == new Vector2(0, 0))
            {
                PlayAnimation("idle");
            }

            if (onGround == false)
            {
                Gravity();
            }
        }


        public override void OnCollision(GameObject other)
        {
            
        }

        public void HandleInput()
        {
            velocity = Vector2.Zero;

            KeyboardState keystate = Keyboard.GetState();
            
            if (keystate.IsKeyDown(Keys.A))
            {
                velocity += new Vector2(-1, 0);
                PlayAnimation("run");
            }

            if (keystate.IsKeyDown(Keys.D))
            {
                velocity += new Vector2(1, 0);
            }

            if (keystate.IsKeyDown(Keys.Space))
            {
                Jump();
            }

            if (keystate.IsKeyDown(Keys.OemComma))
            {
                Attack();
            }

            if (keystate.IsKeyDown(Keys.OemPeriod))
            {
                Shoot();
            }
        }

        public void Jump()
        {
            velocity += new Vector2(0, -1);
            onGround = false;
        }

        public void Attack()
        {

        }

        public void Shoot()
        {

        }

        public void Gravity()
        {
            velocity.Y += gravity;
        }
    }
}
