using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Nessie.Components;

namespace CometsEvacuation.Components
{
    public class Particle
    {
        public float LifeTime;
        public Color Color;
        public float AlphaColor;
        public Vector2 Position;
        public Vector2 Velocity;
        public float Size;
        public float Rotation;
    }
    
    public class ParticleEmitterComponent : Component
    {
        public List<Particle> Particles { get; private set; }

        public Func<Particle> ParticleFactory { get; set; }

        public int MaxParticles { get; set; }
        public bool IsContinuous { get; set; }

        public ParticleEmitterComponent()
        {
            Particles = new List<Particle>();
        }
    }
}