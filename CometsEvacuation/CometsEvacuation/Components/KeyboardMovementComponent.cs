using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nessie.Components;
using Microsoft.Xna.Framework.Input;

namespace CometsEvacuation.Components
{
    [ComponentDependency(typeof(MovableComponent))]
    public class KeyboardMovementComponent : Component
    {
        public Keys Up { get; set; }
        public Keys Down { get; set; }
        public Keys Left { get; set; }
        public Keys Right { get; set; }

        public bool UseXDirection { get; set; }
        public bool UseYDirection { get; set; }

        public override void SetDefaultValues()
        {
            SetToArrowKeys();
            UseXDirection = true;
            UseYDirection = true;
        }

        public void SetToArrowKeys()
        {
            Up = Keys.Up;
            Down = Keys.Down;
            Left = Keys.Left;
            Right = Keys.Right;
        }

        public void SetToWasd()
        {
            Up = Keys.W;
            Down = Keys.S;
            Left = Keys.A;
            Right = Keys.D;
        }
    }
}