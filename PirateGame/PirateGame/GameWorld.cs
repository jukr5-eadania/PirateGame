﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using System.Collections.Generic;
using System.IO;

namespace PirateGame
{
    public class GameWorld : Game
    {
        // Field
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private List<GameObject> gameObjects = new List<GameObject>();
        private Player player = new Player(new Vector2(GameWorld.Width / 2, GameWorld.Height / 2));
        private Texture2D collisionTexture;
        private static List<GameObject> removeObjects = new List<GameObject>();
        public static int Height { get; set; }
        public static int Width { get; set; }
       
        private Dictionary<Vector2, int> tiles;
        private Texture2D textureAtlas;
        private Rectangle destinationRectange;
        private Matrix _translation;
        private SpriteFont UIFont;
        private Background bg;

        

        // Properties
        internal static List<GameObject> RemoveObjects { get => removeObjects; set => removeObjects = value; }
        
         
        // Methods

        
        public GameWorld()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.HardwareModeSwitch = false;
            Window.IsBorderless = false;
            _graphics.IsFullScreen = false;
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
            gameObjects.Add(new Coin(new Vector2(GameWorld.Width/2, GameWorld.Height/2)));
            gameObjects.Add(new Enemy());
            
           

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            collisionTexture = Content.Load<Texture2D>("pixel");

            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.LoadContent(Content);
            }

            bg.LoadContent(Content);

            textureAtlas = Content.Load<Texture2D>("Tiles");
            tiles = LoadMap("../../../Content/Map/TestMap.csv");
            AddTiles(tiles);

            UIFont = Content.Load<SpriteFont>("UIFont");
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.Update(gameTime);
                
                /*foreach (GameObject other in gameObjects)
                {
                    gameObject.CheckCollision(other);
                }*/
            }

            foreach (GameObject atkBox in gameObjects)
            {
                atkBox.Update(gameTime);
                foreach (GameObject other in gameObjects)
                {
                    atkBox.CheckCollision(other);

                }
            }

            foreach (GameObject gameObject in RemoveObjects)
            {
                gameObjects.Remove(gameObject);

            }
            RemoveObjects.Clear();


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
                DrawCollisionBox(gameObject);
                DrawAttackBox(gameObject);


            }
            _spriteBatch.DrawString(UIFont, "Health: " + player.Health, new Vector2((float)(player.Position.X - GameWorld.Width / 2), (float)(player.Position.Y - GameWorld.Height / 2)), Color.Black);
            _spriteBatch.DrawString(UIFont, "Coins: " + player.Coin, new Vector2((float)(player.Position.X - GameWorld.Width / 2), (float)(player.Position.Y - GameWorld.Height / 2) + 15), Color.Black);
            _spriteBatch.DrawString(UIFont, "Ammo: " + player.Ammo, new Vector2((float)(player.Position.X - GameWorld.Width / 2), (float)(player.Position.Y - GameWorld.Height / 2) + 30), Color.Black);
            _spriteBatch.End();

            base.Draw(gameTime);
        }


       private void DrawCollisionBox(GameObject go)
        {
            Rectangle collisionBox = go.collisionBox;
            Rectangle topLine = new Rectangle(collisionBox.X, collisionBox.Y, collisionBox.Width, 1);
            Rectangle bottomLine = new Rectangle(collisionBox.X, collisionBox.Y + collisionBox.Height, collisionBox.Width, 1);
            Rectangle rightLine = new Rectangle(collisionBox.X + collisionBox.Width, collisionBox.Y, 1, collisionBox.Height);
            Rectangle leftLine = new Rectangle(collisionBox.X, collisionBox.Y, 1, collisionBox.Height);

            _spriteBatch.Draw(collisionTexture, topLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            _spriteBatch.Draw(collisionTexture, bottomLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            _spriteBatch.Draw(collisionTexture, rightLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            _spriteBatch.Draw(collisionTexture, leftLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
        }

       private void DrawAttackBox(GameObject atkBox)
        {
            // AttackBox for enemy
            Rectangle attackBox = atkBox.attackBox;
            Rectangle topLine = new Rectangle(attackBox.X, attackBox.Y, attackBox.Width, 1);
            Rectangle bottomLine = new Rectangle(attackBox.X, attackBox.Y + attackBox.Height, attackBox.Width, 1);
            Rectangle rightLine = new Rectangle(attackBox.X + attackBox.Width, attackBox.Y, 1, attackBox.Height);
            Rectangle leftLine = new Rectangle(attackBox.X, attackBox.Y, 1, attackBox.Height);

            _spriteBatch.Draw(collisionTexture, topLine, null, Color.Black, 0, Vector2.Zero, SpriteEffects.None, 1);
            _spriteBatch.Draw(collisionTexture, bottomLine, null, Color.Black, 0, Vector2.Zero, SpriteEffects.None, 1);
            _spriteBatch.Draw(collisionTexture, rightLine, null, Color.Black, 0, Vector2.Zero, SpriteEffects.None, 1);
            _spriteBatch.Draw(collisionTexture, leftLine, null, Color.Black, 0, Vector2.Zero, SpriteEffects.None, 1);
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

                destinationRectange = new((int)item.Key.X * displayTilesize, (int)item.Key.Y * displayTilesize, displayTilesize, displayTilesize);

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

    }
}
