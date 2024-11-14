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
    internal class WorldTile : GameObject
    {
        Texture2D textureAtlas;
        Rectangle destinationRectange;
        Rectangle source;

        public override Rectangle collisionBox
        {
            get => destinationRectange;
        }
        public WorldTile(Texture2D textureAtlas,Rectangle destinationRectange,Rectangle source)
        {
            this.textureAtlas = textureAtlas;
            this.destinationRectange = destinationRectange;
            this.source = source;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textureAtlas, destinationRectange, source, Color.White);
        }
        public override void LoadContent(ContentManager content)
        {
        }

        public override void Update(GameTime gameTime)
        {

        }
    }
}
