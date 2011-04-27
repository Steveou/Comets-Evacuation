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
    [ComponentDependency(typeof(DestroyableComponent))]
    public class ExplodableComponent : Component
    {
        // define the type of particle system which will be created

        public List<string> explodesWith;

        public ParticleFactory ParticleFactory { get; set; }

        public int MaxParticles { get; set; }

        public override void SetDefaultValues()
        {
            explodesWith = new List<string>();
        }
    }
}