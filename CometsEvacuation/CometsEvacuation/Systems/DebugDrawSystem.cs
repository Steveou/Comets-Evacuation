using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nessie.Components;
using Microsoft.Xna.Framework.Graphics;
using CometsEvacuation.Components;
using Nessie.Xna.Components;
using Microsoft.Xna.Framework;

namespace CometsEvacuation.Systems
{
    public class DebugDrawSystem : DrawableSceneSystem
    {
        private SpriteBatch spriteBatch;

        public DebugDrawSystem(SpriteBatch spriteBatch)
        {
            this.spriteBatch = spriteBatch;
        }

        public override void Initialize()
        {
        }

        public override void LoadContent()
        {
        }

        public override void UnloadContent()
        {

        }

        public override void Update(double elapsedSeconds)
        {
        }

        public override void Draw(double elapsedSeconds)
        {
            if (spriteBatch == null)
                return;

            foreach (var obj in SceneManager.GameObjects.Get<DebugCollisionComponent>())
            {
                var components = obj.Get<BasicSpriteComponent, TransformComponent, CollisionComponent>();

                spriteBatch.DrawRectangle(components.Item3.Box.ToRectangle(), Color.Red, 1.0f);
            }
        }
    }
}
