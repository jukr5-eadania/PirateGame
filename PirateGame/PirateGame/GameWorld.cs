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
        private SpriteFont UIFont;
        private Background bg;
        private List<GameObject> gameObjects = new List<GameObject>();
        private Player player = new Player(new Vector2(GameWorld.Width / 2, 325f));
        public static int Height { get; set; }
        public static int Width { get; set; }
        private Dictionary<Vector2, int> tiles;
        // 
        private Texture2D textureAtlas;
        // Matrix used to move camera with player
        private Matrix _translation;
        private static List<GameObject> gameObjectsToAdd = new List<GameObject>();
        private static List<GameObject> gameObjectsToRemove = new List<GameObject>();
        private Texture2D collisionTexture;

        public GameWorld()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.HardwareModeSwitch = false;
            Window.IsBorderless = true;
            _graphics.IsFullScreen = true;
            _graphics.PreferredBackBufferHeight = 1080;
            _graphics.PreferredBackBufferWidth = 1920;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

        }

        protected override void Initialize()
        {
            GameWorld.Height = _graphics.PreferredBackBufferHeight;
            GameWorld.Width = _graphics.PreferredBackBufferWidth;
            bg = new Background();
            gameObjects.Add(player);
            gameObjects.Add(new Coin(new Vector2(130, 280)));
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.LoadContent(Content);
            }

            bg.LoadContent(Content);

            textureAtlas = Content.Load<Texture2D>("Tiles");
            tiles = LoadMap("../../../Content/Map/TestMap.csv");
            AddTiles(tiles);

            collisionTexture = Content.Load<Texture2D>("CollisionTexture");

            UIFont = Content.Load<SpriteFont>("UIFont");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (!player.isAlive())
            {
                Exit();
            }

            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.Update(gameTime);
                foreach (GameObject other in gameObjects)
                {
                    gameObject.CheckCollision(other);
                }
            }



            foreach (GameObject gameObjectToSpawn in gameObjectsToAdd)
            {
                gameObjectToSpawn.LoadContent(Content);
                gameObjects.Add(gameObjectToSpawn);
            }
            gameObjectsToAdd.Clear();

            foreach (GameObject gameObjectToDespawn in gameObjectsToRemove)
            {
                gameObjects.Remove(gameObjectToDespawn);
            }
            gameObjectsToRemove.Clear();

            // Replace vector zero with cameras target
            CalculateCamera(player.Position);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(transformMatrix: _translation);

            bg.Draw(_spriteBatch);

            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.Draw(_spriteBatch);

#if DEBUG
                DrawCollisionBox(gameObject);
#endif
            }
            _spriteBatch.DrawString(UIFont, "Health: " + player.Health, new Vector2((float)(player.Position.X - GameWorld.Width / 2), (float)(player.Position.Y - GameWorld.Height / 2)), Color.White);
            _spriteBatch.DrawString(UIFont, "Treasure: " + player.Coin, new Vector2((float)(player.Position.X - GameWorld.Width / 2), (float)(player.Position.Y - GameWorld.Height / 2) + 30), Color.White);
            _spriteBatch.DrawString(UIFont, "Ammo: " + player.Ammo, new Vector2((float)(player.Position.X - GameWorld.Width / 2), (float)(player.Position.Y - GameWorld.Height / 2) + 60), Color.White);
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

        private void AddTiles(Dictionary<Vector2, int> ground)
        {
            foreach (var item in ground)
            {
                // Adjust to scale level size
                int displayTilesize = 32;
                int numTilesPerRow = 6;
                int pixelTilesize = 32;

                Rectangle destinationRectange = new((int)item.Key.X * displayTilesize, (int)item.Key.Y * displayTilesize, displayTilesize, displayTilesize);

                int x = item.Value % numTilesPerRow;
                int y = item.Value / numTilesPerRow;

                Rectangle source = new(x * pixelTilesize, y * pixelTilesize, pixelTilesize, pixelTilesize);


                gameObjects.Add(new WorldTile(textureAtlas, destinationRectange, source));
            }
        }
        private void CalculateCamera(Vector2 vector)
        {
            var dx = (GameWorld.Width / 2) - vector.X;
            var dy = (GameWorld.Height / 2) - vector.Y;
            _translation = Matrix.CreateTranslation(new Vector3(-vector.X, -vector.Y, 0f)) * Matrix.CreateScale(1, 1, 1) * Matrix.CreateTranslation(new Vector3(GameWorld.Width * 0.5f, GameWorld.Height * 0.5f, 0));
        }

        public static void InstatiateGameObject(GameObject gameObject)
        {
            gameObjectsToAdd.Add(gameObject);
        }

        public static void RemoveGameObject(GameObject gameObject)
        {
            gameObjectsToRemove.Add(gameObject);
        }

        private void DrawCollisionBox(GameObject gameObject)
        {
            Rectangle collisionBox = gameObject.collisionBox;
            Rectangle topLine = new Rectangle(collisionBox.X, collisionBox.Y, collisionBox.Width, 1);
            Rectangle bottomLine = new Rectangle(collisionBox.X, collisionBox.Y + collisionBox.Height, collisionBox.Width, 1);
            Rectangle rightLine = new Rectangle(collisionBox.X + collisionBox.Width, collisionBox.Y, 1, collisionBox.Height);
            Rectangle leftLine = new Rectangle(collisionBox.X, collisionBox.Y, 1, collisionBox.Height);

            _spriteBatch.Draw(collisionTexture, topLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            _spriteBatch.Draw(collisionTexture, bottomLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            _spriteBatch.Draw(collisionTexture, rightLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            _spriteBatch.Draw(collisionTexture, leftLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
        }
    }
}
