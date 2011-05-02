using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nessie.Xna;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using CometsEvacuation.Systems;
using Nessie.Components;

namespace CometsEvacuation.Screens
{
    public class MainScreen : SceneGameScreen
    {
        private SpriteBatch spriteBatch;
        private CometsGameObjectFactory factory;

        private TimeSpan elapsedTime;
        private TimeSpan lastCometSpawn;
        private TimeSpan lastPersonSpawn;
        private TimeSpan lastParachuteSpawn;

        private ParticleEmittersSystem particles;

        private BloodLevel bloodLevel;

        private int currentScore;
        private int currentRescuedScore;

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
            SceneManager.AddSystem(new RotationSystem());
            SceneManager.AddSystem(new GravitationSystem() { Gravitation = 600f, MaxFallSpeed = 600f });

            factory.CreateBackground();

            factory.CreatePaddle();

            bloodLevel = new BloodLevel(Game, 2.0f);

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
            bloodLevel.Unload();
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

            bloodLevel.Draw(spriteBatch);

            spriteBatch.DrawString(Game.Content.Load<SpriteFont>("fonts/standard"), "Time: " + elapsedTime.ToString(@"mm\:ss"), Vector2.Zero, Color.Black);
            spriteBatch.DrawString(Game.Content.Load<SpriteFont>("fonts/standard"), "Stones Destroyed: " + currentScore.ToString(), new Vector2(0, 30), Color.Black);
            spriteBatch.DrawString(Game.Content.Load<SpriteFont>("fonts/standard"), "Aliens Rescued: " + currentRescuedScore.ToString(), new Vector2(0, 60), Color.Black);


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

            if (elapsedTime - lastPersonSpawn > TimeSpan.FromSeconds(1f))
            {
                lastPersonSpawn = elapsedTime;

                factory.CreatePerson();
            }

            if (elapsedTime - lastParachuteSpawn > TimeSpan.FromSeconds(1f))
            {
                lastParachuteSpawn = elapsedTime;

                factory.CreateParachute();
            }
        }

        private void OnCollision(object sender, CollisionEventArgs args)
        {



            if (args.Object1 != null && args.Object2 != null)
            {
                if (CollisionBetween(args.Object1, args.Object2, "paddle", "stones"))
                {
                    currentScore++;
                }
                else if (CollisionBetween(args.Object1, args.Object2, "stones", "persons"))
                {
                    bloodLevel.Raise();
                }


                if (args.Object2.GroupName == "target_coll")
                    currentRescuedScore++;
            }
        }

        private bool CollisionBetween(GameObject object1, GameObject object2, string group1, string group2)
        {
            string name1 = object1.GroupName;
            string name2 = object2.GroupName;

            if ((name1 == group1 && name2 == group2) || (name2 == group1 && name1 == group2))
            {
                return true;
            }

            return false;
        }
    }
}