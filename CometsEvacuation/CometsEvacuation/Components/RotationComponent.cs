using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nessie.Components;
using Microsoft.Xna.Framework;

namespace CometsEvacuation.Components
{
    public class RotationComponent : Component
    {
        public Vector2 Origin { get; set; }

        public float CurrentRotation { get; set; }

        public float RotationSpeed { get; set; }
    }
}