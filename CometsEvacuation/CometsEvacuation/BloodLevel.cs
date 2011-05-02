using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Nessie.Xna;
using Microsoft.Xna.Framework;

namespace CometsEvacuation
{
    public class BloodLevel
    {
        public float Step { get; set; }

        public float CurrentBloodLevel { get { return bloodLevel; } }

        private float bloodLevel;

        private Texture2D bloodTexture;

        private NessieGame game;

        public BloodLevel(NessieGame game, float step)
        {
            Step = step;
            this.game = game;

            bloodTexture = new TextureGenerator(game.GraphicsDevice).Create1x1(Color.White);
        }

        public void Raise()
        {
            bloodLevel += Step;
        }

        public void Unload()
        {
            bloodTexture.Dispose();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Color c = Color.Green;
            c.A = 50;

            spriteBatch.Draw(bloodTexture, new Rectangle(0, (int)(game.ScreenHeight - bloodLevel), game.ScreenWidth, (int)bloodLevel), c);
        }
    }
}
