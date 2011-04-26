using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nessie.Components;
using Microsoft.Xna.Framework;
using Nessie.Xna.Components;
using CometsEvacuation.Components;
using Nessie.Xna;

namespace CometsEvacuation.Systems
{
    /// <summary>
    /// Moves objects and checks for collision.
    /// </summary>
    public class CollisionSystem : SceneSystem
    {
        public event EventHandler<CollisionEventArgs> Collision;

        private List<GameObject> toDestroy;

        public CollisionSystem()
        {
            toDestroy = new List<GameObject>();
        }

        public override void Update(double elapsedSeconds)
        {
            bool collisionHappens = false;

            foreach (var obj in SceneManager.GameObjects.Get<MovableComponent>())
            {
                collisionHappens = false;

                var transform = obj.Get<TransformComponent>();
                var movable = obj.Get<MovableComponent>();
                var collision = obj.Get<CollisionComponent>();

                Vector2 offset = movable.Velocity * movable.Speed * (float)elapsedSeconds;

                if (collision != null)
                {
                    if (offset != Vector2.Zero)
                    {
                        // Check for boundaries component.
                        var boundaries = obj.Get<MovementBoundariesComponent>();
                        if (boundaries != null)
                        {
                            if (!boundaries.Box.Contains(collision.Box, -offset))
                            {
                                HandleCollision(obj, null);
                                collisionHappens = true;
                            }
                        }

                        if (!collisionHappens)
                        {
                            foreach (var obj2 in SceneManager.GameObjects.Get<CollisionComponent>())
                            {
                                if (obj != obj2)
                                {
                                    var collision2 = obj2.Get<CollisionComponent>();

                                    if (collision.Box.Intersects(collision2.Box, offset))
                                    {
                                        HandleCollision(obj, obj2);
                                        collisionHappens = true;
                                        break;
                                    }
                                }
                            }
                        }

                        if (!collisionHappens)
                        {
                            // No Collision happens!
                            transform.Position += offset;
                            collision.UpdatePosition(transform.Position);
                            
                        }
                    }
                }
                else
                {
                    // Object just moves but cannot collide.
                    transform.Position += offset;
                }
            }

            // Delete destroyed objects.
            foreach(var destroy in toDestroy)
            {
                SceneManager.GameObjects.Remove(destroy);
            }
            toDestroy.Clear();
        }

        private void HandleCollision(GameObject object1, GameObject object2)
        {
            if (GameObject.HasComponent<DestroyableComponent>(object1))
                toDestroy.Add(object1);

            if (GameObject.HasComponent<DestroyableComponent>(object2))
                toDestroy.Add(object2);

            // Check whether it's destroyable or explodable
            // If so, well, then destroy them
            if (Collision != null)
                Collision(this, new CollisionEventArgs(object1, object2));
        }
    }
}