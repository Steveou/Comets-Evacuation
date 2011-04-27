using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nessie.Components;
using CometsEvacuation.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nessie.Xna.Components;

namespace CometsEvacuation.Systems
{
    public class Particle
    {
        public Texture2D Texture;
        public float LifeTime;
        public Color Color;
        public float AlphaColor;
        public Vector2 Position;
        public Vector2 Velocity;
        public float Size;
        public float Rotation;

        public Particle(Texture2D texture, float lifeTime, Color color, float alphaColor, Vector2 position, Vector2 velocity, float size, float rotation)
        {
            Texture = texture;
            LifeTime = lifeTime;
            Color = color;
            AlphaColor = alphaColor;
            Position = position;
            Velocity = velocity;
            Size = size;
            Rotation = rotation;
        }
    }
    public class ParticleEmittersSystem : DrawableSceneSystem
    {
        private Random random;
        private List<Particle> particles;

        public int ParticlesCount { get { return particles.Count; } }

        private SpriteBatch spriteBatch;

        public ParticleEmittersSystem(SpriteBatch spriteBatch, CollisionSystem collisionSystem)
        {
            particles = new List<Particle>();
            random = new Random();

            this.spriteBatch = spriteBatch;

            collisionSystem.Collision += new EventHandler<CollisionEventArgs>(collisionSystem_Collision);
        }

        private void collisionSystem_Collision(object sender, CollisionEventArgs e)
        {
            if (GameObject.HasComponent<ExplodableComponent>(e.Object1) && ExplodesWith(e.Object1, e.Object2))
                CreateParticles(e.Object1);

            if (GameObject.HasComponent<ExplodableComponent>(e.Object2) && ExplodesWith(e.Object2, e.Object1))
                CreateParticles(e.Object2);
        }

        private bool ExplodesWith(GameObject object1, GameObject object2)
        {
            var explodable = object1.Get<ExplodableComponent>();

            if (explodable.explodesWith.Count == 0 || explodable.explodesWith.Contains(object2.GroupName))
                return true;

            return false;
        }

        public void CreateParticles(GameObject obj)
        {
            var collision = obj.Get<CollisionComponent>();
            var explodable = obj.Get<ExplodableComponent>();

            var spawnArea = collision.Box.ToRectangle();

            for (int i = 0; i < explodable.MaxParticles; i++)
            {


                AddParticle(explodable.ParticleFactory.CreateParticle(spawnArea, random));

            }
        }

        public void AddParticle(Particle particle)
        {
            particles.Add(particle);
        }

        public override void Update(double elapsedSeconds)
        {
            float elapsed = (float)elapsedSeconds;

            for (int i = 0; i < particles.Count; i++)
            {
                particles[i].LifeTime -= (elapsed);

                particles[i].Position.X += particles[i].Velocity.X * elapsed;
                particles[i].Position.Y += particles[i].Velocity.Y * elapsed;

                float NewAlphaColor = particles[i].Color.A - (particles[i].AlphaColor * elapsed);

                NewAlphaColor = (int)MathHelper.Clamp(NewAlphaColor, 0, 255);

                particles[i].Color = new Color(
                    particles[i].Color.R,
                    particles[i].Color.G,
                    particles[i].Color.B,
                    (int)NewAlphaColor
                    );

             //   particles[i].Color = new Color(1f, 1f, 1f, 0f);

                particles[i].Rotation += elapsed;

                if (particles[i].LifeTime < 0)
                {
                    particles.Remove(particles[i]);

                }

            }
        }

        public override void Draw(double elapsedSeconds)
        {
            // draws the particles
            if (spriteBatch == null)
                return;

            foreach (var particle in particles)
            {
                spriteBatch.Draw(
                    particle.Texture,
                    particle.Position, null,
                    particle.Color,
                    particle.Rotation,
                    new Vector2(particle.Texture.Width / 2, particle.Texture.Height / 2),
                    particle.Size,
                    SpriteEffects.None,
                    0
                    );
            }
        }
    }
}
