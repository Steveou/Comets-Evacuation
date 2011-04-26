using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nessie.Components;
using CometsEvacuation.Systems;
using Nessie.Xna.Components;

namespace CometsEvacuation.Components
{
    [ComponentDependency(typeof(CollisionComponent))]
    [ComponentDependency(typeof(TransformComponent))]
    public class ExplodableComponent : Component
    {
        // define the type of particle system which will be created

        public ParticleFactory ParticleFactory { get; set; }

        public int MaxParticles { get; set; }
    }
}