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

        private Dictionary<Vector2, int> ground;
        private Texture2D textureAtlas;

        private Vector2 pos = new Vector2(-100, -100);
        private Rectangle rec;
        private Rectangle destinationRectange;
        private Matrix _translation;
        public List<Rectangle> tiles = new();

        public GameWorld()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            ground = LoadMap("../../../Content/Map/Map.csv");
            rec = new Rectangle((int)pos.X, (int)pos.Y, 32, 32);
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

            TempMove(gameTime);
            calculateTranslation();
            checkCollision();
            rec = new Rectangle((int)pos.X, (int)pos.Y, 32, 32);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(transformMatrix: _translation);
            tiles.Clear();
            foreach (var item in ground)
            {
                int displayTilesize = 48;
                int numTilesPerRow = 6;
                int pixelTilesize = 32;

                destinationRectange = new((int)item.Key.X * displayTilesize, (int)item.Key.Y * displayTilesize, displayTilesize, displayTilesize);

                int x = item.Value % numTilesPerRow;
                int y = item.Value / numTilesPerRow;

                Rectangle source = new(x * pixelTilesize, y * pixelTilesize, pixelTilesize, pixelTilesize);

                tiles.Add(destinationRectange);
                _spriteBatch.Draw(textureAtlas, destinationRectange, source, Color.White);
            }
            _spriteBatch.Draw(textureAtlas, rec, Color.White);

            _spriteBatch.End();

            base.Draw(gameTime);
        }

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

        private void TempMove(GameTime gameTime)
        {
            KeyboardState keystate = Keyboard.GetState();

            if (keystate.IsKeyDown(Keys.W))
            {
                pos.Y -= 1;
            }
            if (keystate.IsKeyDown(Keys.A))
            {
                pos.X -= 1;
            }
            if (keystate.IsKeyDown(Keys.S))
            {
                pos.Y += 1;
            }
            if (keystate.IsKeyDown(Keys.D))
            {
                pos.X += 1;
            }

        }
        
        private void calculateTranslation()
        {
            var dx = (_graphics.PreferredBackBufferWidth / 2) - pos.X;
            var dy = (_graphics.PreferredBackBufferHeight / 2) - pos.Y;
            _translation = Matrix.CreateTranslation(dx, dy, 0f);
        }

        private void checkCollision()
        {
            bool col = false;
            foreach (Rectangle rectangle in tiles)
            {
                if (rec.Intersects(rectangle))
                {
                     col = true;
                }

            }
            if (col)
            {
                //stop moving
            }
        }
    }
}
