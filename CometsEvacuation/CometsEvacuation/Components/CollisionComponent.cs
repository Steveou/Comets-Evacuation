using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nessie.Components;
using Nessie.Xna;
using Microsoft.Xna.Framework;

namespace CometsEvacuation.Components
{
    [ComponentDependency(typeof(MovableComponent))]
    public class CollisionComponent : Component
    {
        public CollisionBox Box { get; private set; }

        // BoundingBoxes...

        public CollisionComponent()
        {
            Box = new CollisionBox();
        }

        public void UpdatePosition(Vector2 position)
        {
            Box.Left = position.X;
            Box.Top = position.Y;
        }

        public void SetValues(Vector2 position, float width, float height)
        {
            Box.Left = position.X;
            Box.Top = position.Y;

            Box.Width = width;
            Box.Height = height;
        }
    }
}