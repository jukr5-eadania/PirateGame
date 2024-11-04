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
    /// "Enemy" is a sup-class from GameObject.
    /// </summary>
    abstract class Enemy : GameObject
    {
        // Field
        
        private int health;


        // Properties

        // Methods
        public override void LoadContent(ContentManager content)
        {
            // make the idle animation
            sprites = new Texture2D[7];
            for(int i = 0; i < sprites.Length; i++)
            {
                sprites[i] = content.Load<Texture2D>($"Skeleton_White_{i}");
            }
            //set a default sprite
            sprite = sprites[0];
        }
        public void OnCollision (GameObject other)
        {

        }
        public void TakeDamage()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {

        }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }


    }
}
