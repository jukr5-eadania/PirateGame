using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PirateGame
{
    /// <summary>
    /// Bullet class that keeps track of the bullets.
    /// The class is a child of GameObject
    /// Made by: Julius
    /// </summary>
    internal class Bullet : GameObject
    {
        public Bullet(Texture2D sprite, Vector2 position)
        {
            this.sprite = sprite;
            this.Position = position;
            velocity = new Vector2(1, 0);
            speed = 300;
        }

        public override void LoadContent(ContentManager content)
        {
            
        }

        public override void Update(GameTime gameTime)
        {
            Move(gameTime);
        }

        public override void OnCollision(GameObject other)
        {
            if (other is WorldTile)
            {
                GameWorld.RemoveGameObject(this);
            }
        }
    }
}
