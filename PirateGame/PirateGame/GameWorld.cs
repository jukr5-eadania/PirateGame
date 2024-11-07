using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.IO;

namespace PirateGame
{
    public class GameWorld : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Dictionary<Vector2, int> tiles;
        private Texture2D textureAtlas;
        private Rectangle destinationRectange;
        private Matrix _translation;
        public List<Rectangle> collisionTiles = new();

        public GameWorld()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            tiles = LoadMap("../../../Content/Map/TestMap.csv");
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            textureAtlas = Content.Load<Texture2D>("Tiles");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // Replace vector zero with cameras target
            CalculateCamera(Vector2.Zero);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(transformMatrix: _translation);

            DrawMap(tiles);
            
            _spriteBatch.End();

            base.Draw(gameTime);
        }
        /// <summary>
        /// Returns every position and type of tile from given tilemap
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        private Dictionary<Vector2, int> LoadMap(string filepath)
        {
            Dictionary<Vector2, int> result = new();
            StreamReader reader = new(filepath);

            int y = 0;
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] items = line.Split(',');

                for (int x = 0; x < items.Length; x++)
                {
                    if (int.TryParse(items[x], out int value))
                    {
                        if (value > -1)
                        {
                            result[new Vector2(x, y)] = value;
                        }
                    }
                }
                y++;
            }
            return result;
        }

        private void DrawMap(Dictionary<Vector2, int> ground)
        {
            collisionTiles.Clear();
            foreach (var item in ground)
            {
                // Adjust to scale level size
                int displayTilesize = 32;
                int numTilesPerRow = 6;
                int pixelTilesize = 32;

                destinationRectange = new((int)item.Key.X * displayTilesize, (int)item.Key.Y * displayTilesize, displayTilesize, displayTilesize);
                collisionTiles.Add(destinationRectange);

                int x = item.Value % numTilesPerRow;
                int y = item.Value / numTilesPerRow;

                Rectangle source = new(x * pixelTilesize, y * pixelTilesize, pixelTilesize, pixelTilesize);

                _spriteBatch.Draw(textureAtlas, destinationRectange, source, Color.White);
            }
        }
        private void CalculateCamera(Vector2 vector)
        {
            var dx = (_graphics.PreferredBackBufferWidth / 2) - vector.X;
            var dy = (_graphics.PreferredBackBufferHeight / 2) - vector.Y;
            _translation = Matrix.CreateTranslation(dx, dy, 0f);
        }
    }
}
