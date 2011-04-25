using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nessie.Components;
using CometsEvacuation.Components;
using Microsoft.Xna.Framework;

namespace CometsEvacuation.Systems
{
    public class GravitationSystem : SceneSystem
    {
        public float Gravitation { get; set; }

        public float MaxFallSpeed { get; set; }

        public override void Update(double elapsedSeconds)
        {
            foreach (var obj in SceneManager.GameObjects.Get<GravitationComponent>())
            {
                var movable = obj.Get<MovableComponent>();

                float velocityY = movable.Velocity.Y + Gravitation * (float)elapsedSeconds;

                if (velocityY <= -MaxFallSpeed)
                    velocityY = -MaxFallSpeed;
                else if (velocityY >= MaxFallSpeed)
                    velocityY = MaxFallSpeed;

                movable.Velocity = new Vector2(movable.Velocity.X, velocityY);
            }
        }
    }
}
