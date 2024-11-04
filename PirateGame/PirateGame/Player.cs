using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PirateGame
{
    internal class Player : GameObject
    {
        private int health;
        private int damage;

        public Player()
        {

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
            Animation(gameTime);
        }


        public override void OnCollision(GameObject other)
        {

        }

        public void HandleInput()
        {

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
