using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PirateGame
{
    internal class Player : GameObject
    {
        private int health;
        private int damage;

        public Player()
        {
            speed = 100;
        }

        public override void LoadContent(ContentManager content)
        {
            sprites = new Texture2D[5];
            for (int i = 0; i < sprites.Length; i++)
            {
                sprites[i] = content.Load<Texture2D>($"Pirate\\Idle\\idle{i}");
            }

            sprite = sprites[0];
            
            position = new Vector2(GameWorld.Width / 2, GameWorld.Height / 2);
        }

        public override void Update(GameTime gameTime)
        {
            HandleInput();
            Move(gameTime);
            Animation(gameTime);
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

        }

        public void Attack()
        {

        }

        public void Shoot()
        {

        }
    }
}
