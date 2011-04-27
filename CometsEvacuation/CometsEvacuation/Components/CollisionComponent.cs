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

        public Vector2 Origin;

        public List<string> collidesWith;

        public CollisionComponent()
        {
            
        }

        public override void SetDefaultValues()
        {
            Box = new CollisionBox();

            collidesWith = new List<string>();
        }

        public void UpdatePosition(Vector2 position)
        {
            Box.Left = position.X - Origin.X;
            Box.Top = position.Y - Origin.Y;
        }

        public void SetValues(Vector2 position, float width, float height)
        {
            Box.Left = position.X - Origin.X;
            Box.Top = position.Y - Origin.X;

            Box.Width = width;
            Box.Height = height;
        }
    }
}