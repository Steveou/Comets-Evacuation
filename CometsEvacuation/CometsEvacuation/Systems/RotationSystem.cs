using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nessie.Components;
using CometsEvacuation.Components;

namespace CometsEvacuation.Systems
{
    public class RotationSystem : SceneSystem
    {
        public override void Update(double elapsedSeconds)
        {
            foreach (var obj in this.SceneManager.GameObjects.Get<RotationComponent>())
            {
                var rotation = obj.Get<RotationComponent>();

                rotation.CurrentRotation += rotation.RotationSpeed * (float)elapsedSeconds;
            }
        }
    }
}
