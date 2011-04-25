using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nessie.Components;
using Microsoft.Xna.Framework;
using Nessie.Xna.Components;

namespace CometsEvacuation.Components
{
    [ComponentDependency(typeof(TransformComponent))]
    public class MovableComponent : Component
    {
        public Vector2 Velocity { get; set; }
        public float Speed { get; set; }

        public override void SetDefaultValues()
        {
            Velocity = Vector2.Zero;
            Speed = 0.0f;
        }
    }
}