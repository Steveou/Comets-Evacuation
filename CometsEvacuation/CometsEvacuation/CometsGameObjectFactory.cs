using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Nessie.Components;
using Nessie.Xna.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nessie.Xna;
using CometsEvacuation.Components;
using Microsoft.Xna.Framework.Content;
using CometsEvacuation.Systems;

namespace CometsEvacuation
{
    public class CometsGameObjectFactory : GameObjectFactory
    {
        private NessieGame game;
        private Random random;

        public CometsGameObjectFactory(NessieGame game, SceneManager sceneManager)
            : base(sceneManager)
        {
            this.game = game;

            random = new Random();
        }

        public GameObject CreateSprite(string name, string group, Vector2 position, Texture2D texture)
        {
            GameObject sprite = CreateObject(name, group, typeof(BasicSpriteComponent));
            sprite.Get<TransformComponent>().Position = position;
            sprite.Get<TextureComponent>().Texture = texture;

            return sprite;
        }

        public void CreateBackground()
        {
          
            CreateSprite(
                "background",
                "background",
                Vector2.Zero,
                game.Content.Load<Texture2D>("graphics/background")
                );

            CreateSprite(
                "target",
                "background",
                new Vector2(game.ScreenWidth - 50, game.ScreenHeight - 250),
                game.Content.Load<Texture2D>("graphics/target")
                );
        }

        public GameObject CreatePaddle()
        {
            GameObject paddle = CreateObject(
                "paddle",
                "paddle",
                typeof(BasicSpriteComponent), typeof(KeyboardMovementComponent), typeof(MovementBoundariesComponent)
                );

            paddle.Get<TransformComponent>().Position = new Vector2(1024 / 2, 768 - 100);

            paddle.Get<TextureComponent>().Texture = game.Content.Load<Texture2D>("graphics/paddle");

            paddle.Get<MovableComponent>().Speed = 600.0f;
            paddle.Get<KeyboardMovementComponent>().SetToArrowKeys();
            paddle.Get<KeyboardMovementComponent>().UseXDirection = true;
            paddle.Get<MovementBoundariesComponent>().SetToScreenValues(game);

            paddle.Get<CollisionComponent>().SetValues(
                paddle.Get<TransformComponent>().Position,
                paddle.Get<TextureComponent>().Texture.Width,
                paddle.Get<TextureComponent>().Texture.Height
                );

            return paddle;
        }

        public GameObject CreateStone()
        {
            // Later there will be stones with different sizes.

            GameObject stone = CreateObject(
                "stone",
                "stones",
                typeof(BasicSpriteComponent), typeof(MovementBoundariesComponent), typeof(GravitationComponent),
                typeof(DestroyableComponent), typeof(ExplodableComponent)
                );

            stone.Get<TextureComponent>().Texture = game.Content.Load<Texture2D>("graphics/comet1");

            stone.Get<TransformComponent>().Position = new Vector2(
                (float)random.NextDouble() * game.ScreenWidth,
                -stone.Get<TextureComponent>().Texture.Height
                );

            stone.Get<MovableComponent>().Speed = 1.0f;
            stone.Get<MovementBoundariesComponent>().Box = new CollisionBox(
                0, -stone.Get<TextureComponent>().Texture.Height,
                game.ScreenWidth + stone.Get<TextureComponent>().Texture.Width,
                game.ScreenHeight + stone.Get<TextureComponent>().Texture.Height);

            stone.Get<CollisionComponent>().SetValues(
                stone.Get<TransformComponent>().Position,
                stone.Get<TextureComponent>().Texture.Width,
                stone.Get<TextureComponent>().Texture.Height
                );

            stone.Get<ExplodableComponent>().MaxParticles = 40;
            stone.Get<ExplodableComponent>().ParticleFactory = new ParticleFactory(
                new List<Texture2D>() { game.Content.Load<Texture2D>("graphics/comet1"), game.Content.Load<Texture2D>("graphics/comet2") },
                3, 7,
                new Color(255, 255, 255, 255),
                100, 256,
                new Vector2(-100, -100), new Vector2(100, 100),
                0.2f, 0.5f,
                1, 3
                );
            return stone;
        }

        public GameObject CreatePerson()
        {
            throw new NotImplementedException();
        }

        public GameObject CreateParachute()
        {
            throw new NotImplementedException();
        }

        public GameObject CreateItem()
        {
            throw new NotImplementedException();
        }

        public GameObject CreateAmmoWagon()
        {
            throw new NotImplementedException();
        }
    }
}