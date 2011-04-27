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

            GameObject target = CreateObject(
                "target_coll",
                "target_coll",
                typeof(TransformComponent), typeof(CollisionComponent)
                );

            target.Get<TransformComponent>().Position = new Vector2(game.ScreenWidth + 100, game.ScreenHeight - 100);

            target.Get<CollisionComponent>().SetValues(
                target.Get<TransformComponent>().Position,
                100,
                100
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
            paddle.Get<CollisionComponent>().collidesWith.Add("stones");
            paddle.Get<CollisionComponent>().collidesWith.Add("target_coll");

            return paddle;
        }

        public GameObject CreateStone()
        {
            // Later there will be stones with different sizes.

            GameObject stone = CreateObject(
                "stone",
                "stones",
                typeof(BasicSpriteComponent), typeof(MovementBoundariesComponent), typeof(GravitationComponent),
                typeof(ExplodableComponent),
                typeof(RotationComponent)
                );

            stone.Get<TextureComponent>().Texture = game.Content.Load<Texture2D>("graphics/comet1");

            stone.Get<TransformComponent>().Position = new Vector2(
                (float)random.NextDouble() * game.ScreenWidth,
                -stone.Get<TextureComponent>().Texture.Height
                );

            stone.Get<MovableComponent>().Speed = 1.0f;
            stone.Get<MovementBoundariesComponent>().Box = new CollisionBox(
                0, -stone.Get<TextureComponent>().Texture.Height * 2,
                game.ScreenWidth + stone.Get<TextureComponent>().Texture.Width,
                game.ScreenHeight + stone.Get<TextureComponent>().Texture.Height * 2);

            stone.Get<CollisionComponent>().Origin = stone.Get<TextureComponent>().Texture.GetCenter();
            stone.Get<CollisionComponent>().SetValues(
                stone.Get<TransformComponent>().Position,
                stone.Get<TextureComponent>().Texture.Width,
                stone.Get<TextureComponent>().Texture.Height
                );
            stone.Get<CollisionComponent>().collidesWith.Add("persons");
            stone.Get<CollisionComponent>().collidesWith.Add("paddle");

            stone.Get<RotationComponent>().Origin = stone.Get<CollisionComponent>().Origin;
            stone.Get<RotationComponent>().RotationSpeed = random.Next(-17, 17);
            stone.Get<RotationComponent>().CurrentRotation = random.Next(0, 2);

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
            GameObject person = CreateObject(
                "person",
                "persons",
                typeof(BasicSpriteComponent), 
                typeof(ExplodableComponent), typeof(RotationComponent)
                );


            person.Get<TextureComponent>().Texture = game.Content.Load<Texture2D>("graphics/alien");

            person.Get<TransformComponent>().Position = new Vector2(
                -person.Get<TextureComponent>().Texture.Width,
                game.ScreenHeight - person.Get<TextureComponent>().Texture.Height + person.Get<TextureComponent>().Texture.Height / 2
                );
            
            person.Get<MovableComponent>().Speed = random.Next(130, 270);
            person.Get<MovableComponent>().Velocity = new Vector2(1.0f, 0);

            /*
            person.Get<MovementBoundariesComponent>().Box = new CollisionBox(
                -person.Get<TextureComponent>().Texture.Width * 2,
                -person.Get<TextureComponent>().Texture.Height * 2,
                game.ScreenWidth + person.Get<TextureComponent>().Texture.Width,
                game.ScreenHeight + person.Get<TextureComponent>().Texture.Height * 2);
            */
            person.Get<CollisionComponent>().Origin = person.Get<TextureComponent>().Texture.GetCenter();
            person.Get<CollisionComponent>().SetValues(
                person.Get<TransformComponent>().Position,
                person.Get<TextureComponent>().Texture.Width,
                person.Get<TextureComponent>().Texture.Height
                );
            person.Get<CollisionComponent>().collidesWith.Add("stones");
            person.Get<CollisionComponent>().collidesWith.Add("target_coll");

            person.Get<ExplodableComponent>().MaxParticles = 80;
            person.Get<ExplodableComponent>().ParticleFactory = new ParticleFactory(
                new List<Texture2D>() { game.Content.Load<Texture2D>("graphics/blood") },
                3, 7,
                Color.White,
                100, 256,
                new Vector2(-200, -30), new Vector2(200, 150),
                0.2f, 0.4f,
                1, 3
                );
            person.Get<ExplodableComponent>().explodesWith.Add("stones");

            person.Get<RotationComponent>().Origin = person.Get<CollisionComponent>().Origin;
            person.Get<RotationComponent>().RotationSpeed = random.Next(2, 7);
            person.Get<RotationComponent>().CurrentRotation = random.Next(0, 2);

            return person;
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