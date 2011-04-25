using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Nessie.Components;
using CometsEvacuation.Components;

namespace CometsEvacuation.Systems
{
    
    public class KeyboardMovementSystem : SceneSystem
    {
        public override void Update(double elapsedSeconds)
        {
            KeyboardState state = Keyboard.GetState();

            foreach (var obj in SceneManager.GameObjects.Get<KeyboardMovementComponent>())
            {
                var keyboardMovement = obj.Get<KeyboardMovementComponent>();

                Vector2 velocity = Vector2.Zero;

                if (keyboardMovement.UseXDirection)
                {
                    if (state.IsKeyDown(keyboardMovement.Left))
                    {
                        velocity.X = -1;
                    }
                    if (state.IsKeyDown(keyboardMovement.Right))
                    {
                        velocity.X = 1;
                    }
                }

                if (keyboardMovement.UseYDirection)
                {
                    if (state.IsKeyDown(keyboardMovement.Up))
                    {
                        velocity.Y = -1;
                    }
                    if (state.IsKeyDown(keyboardMovement.Down))
                    {
                        velocity.Y = 1;
                    }
                }

                obj.Get<MovableComponent>().Velocity = velocity;
            }
        }
    }
}