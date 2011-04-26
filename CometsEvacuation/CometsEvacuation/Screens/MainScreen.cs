using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nessie.Xna;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Nessie.Xna.Components.Systems;
using CometsEvacuation.Systems;

namespace CometsEvacuation.Screens
{
    public class MainScreen : SceneGameScreen
    {
        private SpriteBatch spriteBatch;
        private CometsGameObjectFactory factory;

        private TimeSpan elapsedTime;
        private TimeSpan lastCometSpawn;

        private ParticleEmittersSystem particles;

        private int currentScore;

        public MainScreen(NessieGame game)
            : base(game)
        {

        }

        public override void Initialize()
        {
            elapsedTime = TimeSpan.Zero;

            base.Initialize();
        }

        public override void LoadContent()
        {
            spriteBatch = new SpriteBatch(Game.GraphicsDevice);
            factory = new CometsGameObjectFactory(Game, SceneManager);

            SetMockTextures();

            
            SceneManager.AddSystem(new SpriteSystem(spriteBatch));

            var collisionSystem = new CollisionSystem();
            collisionSystem.Collision += OnCollision;

            SceneManager.AddSystem(collisionSystem);

            particles = new ParticleEmittersSystem(spriteBatch, collisionSystem);

            SceneManager.AddSystem(particles);
            SceneManager.AddSystem(new KeyboardMovementSystem());
            SceneManager.AddSystem(new GravitationSystem() { Gravitation = 600f, MaxFallSpeed = 600f });

            factory.CreateBackground();

            factory.CreatePaddle();

            base.LoadContent();
        }

        private void SetMockTextures()
        {
            MockContentManager content = Game.Content as MockContentManager;

            if (content != null)
            {
                content.SetMockTexture("graphics/paddle", 200, 100, Color.Red);
                content.SetMockTexture("graphics/target", 50, 250, Color.Gray);
                content.SetMockTexture("graphics/stone", 50, 50, Color.Brown);
            }
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(double elapsedSeconds)
        {
            KeyboardState state = Keyboard.GetState();

            elapsedTime = elapsedTime.Add(TimeSpan.FromSeconds(elapsedSeconds));

            UpdateSpawn(elapsedSeconds);

            Game.Window.Title = "Particles: " + particles.ParticlesCount.ToString();

            base.Update(elapsedSeconds);
        }

        public override void Draw(double elapsedSeconds)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);

            base.Draw(elapsedSeconds);

            spriteBatch.DrawString(Game.Content.Load<SpriteFont>("fonts/standard"), currentScore.ToString(), Vector2.Zero, Color.Black);

            spriteBatch.End();
        }

        private void UpdateSpawn(double elapsedSeconds)
        {
            // From time to time we create a new comet.

            // Kometenauwurf steigt linear mit der Zeit an.

            //    if(elapsedTime)

            Random random = new Random();
            //      random.Next()

            if (elapsedTime - lastCometSpawn > TimeSpan.FromSeconds(0.2f))
            {
                lastCometSpawn = elapsedTime;

                factory.CreateStone();
            }


        }

        private void OnCollision(object sender, CollisionEventArgs args)
        {
            if (args.Object1 != null && args.Object2 != null)
            {
                string name1 = args.Object1.GroupName;
                string name2 = args.Object2.GroupName;

                if ((name1 == "paddle" && name2 == "stones") || (name2 == "paddle" && name1 == "stones"))
                {
                    currentScore++;
                }
            }
        }
    }
}