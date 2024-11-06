using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PirateGame
{
    internal class Player : GameObject
    {
        private int health;
        private int damage;
        private bool onGround = true;
        private int combo;

        public Player()
        {
            speed = 100;
        }

        public override void LoadContent(ContentManager content)
        {
            position = new Vector2(GameWorld.Width / 2, GameWorld.Height / 2);

            //Loading atk1 sprites
            Texture2D[] atk1 = new Texture2D[6];

            for (int i = 0; i < atk1.Length; i++)
            {
                atk1[i] = content.Load<Texture2D>($"Pirate/Atk1/atk{i}");
            }

            AddAnimation(new Animation(atk1, "pirate_atk1", 10));

            //Loading atk2 sprites
            Texture2D[] atk2 = new Texture2D[6];

            for (int i = 0; i < atk2.Length; i++)
            {
                atk2[i] = content.Load<Texture2D>($"Pirate/Atk2/atk{i}");
            }

            AddAnimation(new Animation(atk2, "pirate_atk2", 10));

            //Loading atk3 sprites
            Texture2D[] atk3 = new Texture2D[6];

            for (int i = 0; i < atk3.Length; i++)
            {
                atk3[i] = content.Load<Texture2D>($"Pirate/Atk3/atk{i}");
            }

            AddAnimation(new Animation(atk3, "pirate_atk3", 10));

            //Loading death sprites
            Texture2D[] death = new Texture2D[4];

            for (int i = 0; i < death.Length; i++)
            {
                death[i] = content.Load<Texture2D>($"Pirate/Death/death{i}");
            }

            AddAnimation(new Animation(death, "pirate_death", 10));

            //Loading fall sprite
            Texture2D[] fall = new Texture2D[1];

            for (int i = 0; i < fall.Length; i++)
            {
                fall[i] = content.Load<Texture2D>("Pirate/Fall/fall");
            }

            AddAnimation(new Animation(fall, "pirate_fall", 10));

            //Loading gun_in sprites
            Texture2D[] gun_in = new Texture2D[5];

            for (int i = 0; i < gun_in.Length; i++)
            {
                gun_in[i] = content.Load<Texture2D>($"Pirate/Gun_in/gun_in{i}");
            }

            AddAnimation(new Animation(gun_in, "pirate_gun_in", 10));

            //Loading gun_out sprites
            Texture2D[] gun_out = new Texture2D[6];

            for (int i = 0; i < gun_out.Length; i++)
            {
                gun_out[i] = content.Load<Texture2D>($"Pirate/Gun_Out/gun_out{i}");
            }

            AddAnimation(new Animation(gun_out, "pirate_gun_out", 10));

            //Loading hit sprites
            Texture2D[] hit = new Texture2D[3];

            for (int i = 0; i < hit.Length; i++)
            {
                hit[i] = content.Load<Texture2D>($"Pirate/Hit/hit{i}");
            }

            AddAnimation(new Animation(hit, "pirate_hit", 10));

            //Loading idle sprites
            Texture2D[] idle = new Texture2D[5];

            for (int i = 0; i < idle.Length; i++)
            {
                idle[i] = content.Load<Texture2D>($"Pirate/Idle/idle{i}");
            }

            AddAnimation(new Animation(idle, "pirate_idle", 10));

            //Loading jump sprites
            Texture2D[] jump = new Texture2D[2];

            for (int i = 0; i < jump.Length; i++)
            {
                jump[i] = content.Load<Texture2D>($"Pirate/Jump/jump{i}");
            }

            AddAnimation(new Animation(jump, "pirate_jump", 10));

            //Loading jump_dust sprites
            Texture2D[] jump_dust = new Texture2D[3];

            for (int i = 0; i < jump_dust.Length; i++)
            {
                jump_dust[i] = content.Load<Texture2D>($"Pirate/Jump_Dust/jump_dust{i}");
            }

            AddAnimation(new Animation(jump_dust, "pirate_jump_dust", 10));

            //Loading land sprites
            Texture2D[] land = new Texture2D[2];

            for (int i = 0; i < land.Length; i++)
            {
                land[i] = content.Load<Texture2D>($"Pirate/Land/land{i}");
            }

            AddAnimation(new Animation(land, "pirate_land", 10));

            //Loading land_dust sprites
            Texture2D[] land_dust = new Texture2D[6];

            for (int i = 0; i < land_dust.Length; i++)
            {
                land_dust[i] = content.Load<Texture2D>($"Pirate/Land_Dust/land_dust{i}");
            }

            AddAnimation(new Animation(land_dust, "pirate_land_dust", 10));

            //Loading run sprites
            Texture2D[] run = new Texture2D[6];

            for (int i = 0; i < run.Length; i++)
            {
                run[i] = content.Load<Texture2D>($"Pirate/Run/run{i}");
            }

            AddAnimation(new Animation(run, "pirate_run", 10));

            //Loading shoot sprites
            Texture2D[] shoot = new Texture2D[5];

            for (int i = 0; i < shoot.Length; i++)
            {
                shoot[i] = content.Load<Texture2D>($"Pirate/Shoot/shoot{i}");
            }

            AddAnimation(new Animation(shoot, "pirate_shoot", 10));

            //Loading shoot_without_fire sprites
            Texture2D[] shoot_without_fire = new Texture2D[5];

            for (int i = 0; i < shoot_without_fire.Length; i++)
            {
                shoot_without_fire[i] = content.Load<Texture2D>($"Pirate/Shoot_Without_Fire/shoot_without_fire{i}");
            }

            AddAnimation(new Animation(shoot_without_fire, "shoot_without_fire", 10));

            PlayAnimation("pirate_idle");
        }

        public override void Update(GameTime gameTime)
        {
            HandleInput();
            Move(gameTime);
            Animation(gameTime);
        }

        public override void OnCollision(GameObject other)
        {

        }

        public void HandleInput()
        {
            velocity = Vector2.Zero;

            KeyboardState keystate = Keyboard.GetState();
            
            if (keystate.IsKeyDown(Keys.A))
            {
                velocity += new Vector2(-1, 0);
                spriteEffects = SpriteEffects.FlipHorizontally;
                PlayAnimation("pirate_run");
            }

            if (keystate.IsKeyDown(Keys.D))
            {
                velocity += new Vector2(1, 0);
                spriteEffects = SpriteEffects.None;
                PlayAnimation("pirate_run");
            }

            if (keystate.IsKeyDown(Keys.Space) && onGround)
            {
                Jump();
            }

            if (keystate.IsKeyDown(Keys.OemComma))
            {
                Attack();
            }

            if (keystate.IsKeyDown(Keys.OemPeriod))
            {
                Shoot();
            }

            if (!onGround)
            {
                PlayAnimation("pirate_fall");
                velocity += new Vector2(0, 1);
            }
        }

        public void Jump()
        {
            PlayAnimation("pirate_jump");
            velocity += new Vector2(0, -1);
            onGround = false;
        }

        public void Attack()
        {
            combo++;
            LoopAnimation = false;
            PlayAnimation("pirate_atk1");
            if (combo == 1)
            {
                PlayAnimation("pirate_atk2");
            }
        }

        public void Shoot()
        {

        }
    }
}
