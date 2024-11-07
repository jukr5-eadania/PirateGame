﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpDX.Direct3D9;
using System.Collections.Generic;

namespace PirateGame
{
    public class GameWorld : Game
    {
        // Field
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private List<GameObject> gameObjects = new List<GameObject>();
        private static List<GameObject> removeObjects = new List<GameObject>();
        
        public static int Height { get; set; }
        public static int Width { get; set; }

        private Texture2D collisionTexture;
                

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
            gameObjects.Add(new Player());
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
            foreach (GameObject go in gameObjects)
            {
                go.Update(gameTime);
                foreach (GameObject other in gameObjects)
                {
                    go.CheckCollision(other);

                }
            }

            foreach (GameObject gameObject in RemoveObjects)
            {
                gameObjects.Remove(gameObject);

            }
            RemoveObjects.Clear();


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            foreach (GameObject gameObject in gameObjects)
            {
                gameObject.Draw(_spriteBatch);
                DrawCollisionBox(gameObject);
                DrawAttackBox(gameObject);

            }
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

       private void DrawAttackBox(GameObject gameObject)
        {
            // AttackBox for enemy
            Rectangle attackBox = gameObject.attackBox;
            Rectangle topLine = new Rectangle(attackBox.X, attackBox.Y, attackBox.Width, 1);
            Rectangle bottomLine = new Rectangle(attackBox.X, attackBox.Y + attackBox.Height, attackBox.Width, 1);
            Rectangle rightLine = new Rectangle(attackBox.X + attackBox.Width, attackBox.Y, 1, attackBox.Height);
            Rectangle leftLine = new Rectangle(attackBox.X, attackBox.Y, 1, attackBox.Height);

            _spriteBatch.Draw(collisionTexture, topLine, null, Color.Yellow, 0, Vector2.Zero, SpriteEffects.None, 1);
            _spriteBatch.Draw(collisionTexture, bottomLine, null, Color.Yellow, 0, Vector2.Zero, SpriteEffects.None, 1);
            _spriteBatch.Draw(collisionTexture, rightLine, null, Color.Yellow, 0, Vector2.Zero, SpriteEffects.None, 1);
            _spriteBatch.Draw(collisionTexture, leftLine, null, Color.Yellow, 0, Vector2.Zero, SpriteEffects.None, 1);
        }


    }
}
