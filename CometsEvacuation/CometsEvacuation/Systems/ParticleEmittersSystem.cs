using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nessie.Components;
using CometsEvacuation.Components;

namespace CometsEvacuation.Systems
{
    public class ParticleEmittersSystem : DrawableSceneSystem
    {
        private List<Particle> particles;

        public ParticleEmittersSystem()
        {
            particles = new List<Particle>();
        }

        public override void Update(double elapsedSeconds)
        {
            foreach (var obj in SceneManager.GameObjects.Get<ParticleEmitterComponent>())
            {

            }
        }

        public override void Draw(double elapsedSeconds)
        {
            // draws the particles
        }
    }
}
