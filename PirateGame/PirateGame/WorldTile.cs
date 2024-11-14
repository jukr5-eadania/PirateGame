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
    /// WorldTile class that draws the tiles of the game
    /// Made by: Mads
    /// </summary>
    internal class WorldTile : GameObject
    {
        Texture2D textureAtlas;
        Rectangle destinationRectange;
        Rectangle source;

        public override Rectangle collisionBox
        {
            get => destinationRectange;
        }

        /// <summary>
        /// Contructor of the tiles used to make the level
        /// </summary>
        /// <param name="textureAtlas"></param>
        /// <param name="destinationRectange"></param>
        /// <param name="source"></param>
        public WorldTile(Texture2D textureAtlas,Rectangle destinationRectange,Rectangle source)
        {
            this.textureAtlas = textureAtlas;
            this.destinationRectange = destinationRectange;
            this.source = source;

        }

        /// <summary>
        /// Draws the sprites
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textureAtlas, destinationRectange, source, Color.White);
        }

        /// <summary>
        /// load the sprites and add the animations they need
        /// </summary>
        /// <param name="content"></param>
        public override void LoadContent(ContentManager content)
        {

        }

        /// <summary>
        /// The main loop of the tiles
        /// </summary>
        /// <param name="gameTime">Takes a GameTime that provides the timespan since last call to update</param>
        public override void Update(GameTime gameTime)
        {

        }
    }
}
