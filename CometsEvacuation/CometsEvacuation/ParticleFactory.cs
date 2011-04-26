using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CometsEvacuation.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CometsEvacuation
{
    public class ParticleFactory
    {
        private List<Texture2D> textures;
        private float minLifetime;
        private float maxLifetime;
        private Color color;
        private float minAlphaColor;
        private float maxAlphaColor;
        private Vector2 minVelocity;
        private Vector2 maxVelocity;
        private float minSize;
        private float maxSize;
        private float minRotation;
        private float maxRotation;

        public ParticleFactory()
        {
            textures = new List<Texture2D>();
        }


        public ParticleFactory(List<Texture2D> textures, float minLifetime, float maxLifetime, Color color, float minAlphaColor, float maxAlphaColor, Vector2 minVelocity, Vector2 maxVelocity, float minSize, float maxSize, float minRotation, float maxRotation)
            : this()
        {
            this.textures = textures;
            this.minLifetime = minLifetime;
            this.maxLifetime = maxLifetime;
            this.color = color;
            this.minAlphaColor = minAlphaColor;
            this.maxAlphaColor = maxAlphaColor;
            this.minVelocity = minVelocity;
            this.maxVelocity = maxVelocity;
            this.minSize = minSize;
            this.maxSize = maxSize;
            this.minRotation = minRotation;
            this.maxRotation = maxRotation;
        }

        public Particle CreateParticle(Rectangle spawnArea, Random random)
        {
            return new Particle(
                textures[random.Next(textures.Count - 1)],
                random.Next((int)minLifetime, (int)maxLifetime),
                color,
                random.Next((int)minAlphaColor, (int)maxAlphaColor),
                new Vector2(random.Next(spawnArea.Left, spawnArea.Right), random.Next(spawnArea.Top, spawnArea.Bottom)),
                new Vector2(random.Next((int)minVelocity.X, (int)maxVelocity.X), random.Next((int)minVelocity.Y, (int)maxVelocity.Y)),
                (float)(random.NextDouble() * (maxSize - minSize) + minSize),
                random.Next((int)minRotation, (int)maxRotation)
                );
        }
    }
}