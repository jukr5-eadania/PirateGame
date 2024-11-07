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
    internal class Enemy : GameObject
    {
        private int heatlh;

        public override void LoadContent(ContentManager content)
        {
            throw new NotImplementedException();
        }

        public override void Update(GameTime gameTime)
        {
            throw new NotImplementedException();
        }

        public override void OnCollision(GameObject other)
        {
            base.OnCollision(other);
        }

        public override void TakeDamage()
        {

        }

        public void Patrol()
        {

        }
    }
}
