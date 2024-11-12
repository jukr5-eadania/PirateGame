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
    internal class Background : GameObject
    {
        public override void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("Background");
        }

        public override void Update(GameTime gameTime)
        {
            
        }
    }
}
