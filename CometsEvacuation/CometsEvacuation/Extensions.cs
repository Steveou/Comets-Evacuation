using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace CometsEvacuation
{
    /// <summary>
    /// Extension methods for the SpriteBatch class.
    /// </summary>
    public static class SpriteBatchExtensions
    {
        private static Texture2D texture;

        private static float a = 0.0f;

        /// <summary>
        ///  Adds a line to the batch of sprites to be rendered.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="position1">The start position of the line.</param>
        /// <param name="position2">The end position of the line.</param>
        /// <param name="color">The color of the line.</param>
        /// <param name="size">The size.</param>
        public static void DrawLine(this SpriteBatch instance, Vector2 position1, Vector2 position2, Color color, float size)
        {
            float distance = Vector2.Distance(position1, position2);
            float angle = (float)Math.Atan2((double)(position2.Y - position1.Y), (double)(position2.X - position1.X));

            // If texture is null we create a new
            if (texture == null)
            {
                CreateTexture(instance.GraphicsDevice);
            }
            instance.Draw(texture, position1, null, color, angle, Vector2.Zero, new Vector2(distance, size), SpriteEffects.None, 1);
        }

        public static void DrawRectangle(this SpriteBatch instance, Rectangle rectangle, Color color, float size)
        {
            DrawLine(instance, new Vector2(rectangle.X, rectangle.Y), new Vector2(rectangle.Right, rectangle.Y), color, size);
            DrawLine(instance, new Vector2(rectangle.X + size, rectangle.Y), new Vector2(rectangle.X + size, rectangle.Bottom), color, size);
            DrawLine(instance, new Vector2(rectangle.Right, rectangle.Y), new Vector2(rectangle.Right, rectangle.Bottom), color, size);
            DrawLine(instance, new Vector2(rectangle.X, rectangle.Bottom), new Vector2(rectangle.Right, rectangle.Bottom), color, size);
        }

        public static void DrawFilledRectangle(this SpriteBatch instance, Rectangle rectangle, Color color)
        {
            // If texture is null we create a new
            if (texture == null)
            {
                CreateTexture(instance.GraphicsDevice);
            }
            instance.Draw(texture, rectangle, color);
        }

        public static void DrawCornerRectangle(this SpriteBatch instance, Rectangle rectangle, Color color, float size, float length)
        {
            // Left-top

            DrawLine(instance,
                new Vector2(rectangle.X - size, rectangle.Y - size),
                new Vector2(rectangle.X + length, rectangle.Y - size),
                color, size);

            DrawLine(instance,
                new Vector2(rectangle.X, rectangle.Y),
                new Vector2(rectangle.X, rectangle.Y + length),
                color, size);

            // Right-top
            DrawLine(instance,
                new Vector2(rectangle.Right, rectangle.Y),
                new Vector2(rectangle.Right - length, rectangle.Y),
                color, size);

            DrawLine(instance,
                new Vector2(rectangle.Right + size, rectangle.Y - size),
                new Vector2(rectangle.Right + size, rectangle.Y + length),
                color, size);

            // Left-bottom
            DrawLine(instance,
                new Vector2(rectangle.X, rectangle.Bottom),
                new Vector2(rectangle.X + length, rectangle.Bottom),
                color, size);

            DrawLine(instance,
                new Vector2(rectangle.X - size, rectangle.Bottom + size),
                new Vector2(rectangle.X - size, rectangle.Bottom - length),
                color, size);

            // right-bottom
            DrawLine(instance,
                new Vector2(rectangle.Right + size, rectangle.Bottom + size),
                new Vector2(rectangle.Right - length, rectangle.Bottom + size),
                color, size);

            DrawLine(instance,
                new Vector2(rectangle.Right, rectangle.Bottom),
                new Vector2(rectangle.Right, rectangle.Bottom - length),
                color, size);
        }

        public static void DrawPoint(this SpriteBatch instance, Vector2 position, Color color, float size)
        {
            if (texture == null)
            {
                CreateTexture(instance.GraphicsDevice);
            }
            instance.Draw(texture, position, null, color, 0.0f, Vector2.Zero, size, SpriteEffects.None, 0);
        }

        private static void CreateTexture(GraphicsDevice device)
        {
            texture = new Texture2D(device, 1, 1, false, SurfaceFormat.Color);

            // Set color to white
            Color[] data = new Color[1];
            texture.GetData<Color>(data);
            data[0] = new Color(255, 255, 255, 255);
            texture.SetData<Color>(data);
        }


        public static void DrawStretched(this SpriteBatch instance, Texture2D texture, Vector2 position1, Vector2 position2, Color color, float width)
        {
            float distance = Vector2.Distance(position1, position2);
            float angle = (float)Math.Atan2((double)(position2.Y - position1.Y), (double)(position2.X - position1.X)) * (float)(MathHelper.Pi * 2000);
            angle = a;
            a++;

            Vector2 scale = new Vector2(
                1.0f,
                distance / texture.Width);

            instance.Draw(texture, position1, null, color, angle, new Vector2(texture.Width / 2, 0), scale, SpriteEffects.None, 1);

        }
    }
}
