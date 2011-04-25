using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nessie.Components;
using Nessie.Xna;
using Microsoft.Xna.Framework;

namespace CometsEvacuation.Components
{
    /// <summary>
    /// Limits the area where an object can move.
    /// </summary>
    [ComponentDependency(typeof(CollisionComponent))]
    public class MovementBoundariesComponent : Component
    {
        public CollisionBox Box { get; set; }

        public MovementBoundariesComponent()
        {
            Box = new CollisionBox();
        }

        public void SetToScreenValues(NessieGame game)
        {
            Box.Left = 0;
            Box.Top = 0;
            Box.Width = game.ScreenWidth;
            Box.Height = game.ScreenHeight;
        }
    }
}