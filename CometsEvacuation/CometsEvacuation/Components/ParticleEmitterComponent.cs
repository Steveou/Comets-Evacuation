using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Nessie.Components;
using Microsoft.Xna.Framework.Graphics;
using CometsEvacuation.Systems;

namespace CometsEvacuation.Components
{
    public class ParticleEmitterComponent : Component
    {
        public ParticleFactory ParticleFactory { get; set; }

        public int MaxParticles { get; set; }
        public bool IsContinuous { get; set; }
    }
}