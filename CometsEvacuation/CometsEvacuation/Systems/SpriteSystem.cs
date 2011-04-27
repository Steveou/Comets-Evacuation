using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nessie.Components;
using Microsoft.Xna.Framework.Graphics;
using Nessie.Xna.Components;
using CometsEvacuation.Components;
using Microsoft.Xna.Framework;

namespace CometsEvacuation.Systems
{
    public class SpriteSystem : DrawableSceneSystem
    {
        private SpriteBatch spriteBatch;

        public SpriteSystem(SpriteBatch spriteBatch)
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

            foreach (var obj in SceneManager.GameObjects.Get<BasicSpriteComponent>())
            {
                var components = obj.Get<BasicSpriteComponent, TransformComponent, TextureComponent>();

                var rotation = obj.Get<RotationComponent>();

                if(rotation != null)
                {
                    spriteBatch.Draw(
                        components.Item3.Texture,
                        components.Item2.Position,
                        null,
                        components.Item1.Color,
                        rotation.CurrentRotation,
                        rotation.Origin,
                        1.0f,
                        components.Item1.Effects,
                        components.Item1.Depth
                        );
                }
                else
                {
                    spriteBatch.Draw(
                        components.Item3.Texture,
                        components.Item2.Position,
                        null,
                        components.Item1.Color,
                        0.0f,
                        Vector2.Zero,
                        1.0f,
                        components.Item1.Effects,
                        components.Item1.Depth
                        );
                }
            }
        }
    }
}
