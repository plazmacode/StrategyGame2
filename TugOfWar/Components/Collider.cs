using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace TugOfWar

{
    public class Collider : Component
    {
        private Lazy<List<RectangleData>> rectangles;

        private bool loaded = false;

        // private List<RectangleData> rectangles = new List<RectangleData>();

        private Texture2D texture;

        private SpriteRenderer spriteRenderer;

        public CollisionEvent CollisionEvent { get; set; } = new CollisionEvent();

        public override void Start()
        {
            rectangles = new Lazy<List<RectangleData>>(() => CreateRectangles());

            spriteRenderer = (SpriteRenderer)GameObject.GetComponent<SpriteRenderer>();

            texture = GameWorld.Instance.Content.Load<Texture2D>("pixel");
        }

        public Rectangle CollisionBox
        {
            get
            {
                return new Rectangle
                    (
                        (int)(GameObject.Transform.Position.X - spriteRenderer.Sprite.Width / 2),
                        (int)(GameObject.Transform.Position.Y - spriteRenderer.Sprite.Height / 2),
                        spriteRenderer.Sprite.Width,
                        spriteRenderer.Sprite.Height
                    );
            }
        }

        public override void Update()
        {
            UpdatePixelCollider();

            CheckCollision();
        }

        private List<RectangleData> CreateRectangles()
        {
            loaded = true;

            List<RectangleData> pixels = new List<RectangleData>();

            List<Color[]> lines = new List<Color[]>();

            for (int y = 0; y < spriteRenderer.Sprite.Height; y++)
            {
                Color[] colors = new Color[spriteRenderer.Sprite.Width];
                spriteRenderer.Sprite.GetData(0, new Rectangle(0, y, spriteRenderer.Sprite.Width, 1), colors, 0, spriteRenderer.Sprite.Width);
                lines.Add(colors);
            }

            for (int y = 0; y < lines.Count; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
                    if (lines[y][x].A != 0)
                    {
                        if ((x == 0)
                            || (x == lines[y].Length)
                            || (x > 0 && lines[y][x - 1].A == 0)
                            || (x < lines[y].Length - 1 && lines[y][x + 1].A == 0)
                            || (y == 0)
                            || (y > 0 && lines[y - 1][x].A == 0)
                            || (y < lines.Count - 1 && lines[y + 1][x].A == 0))
                        {

                            RectangleData rd = new RectangleData(x, y);

                            pixels.Add(rd);
                        }
                    }
                }
            }

            return pixels;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (loaded)
            {
                foreach (RectangleData rd in rectangles.Value)
                {
                    DrawRectangle(rd.Rectangle, spriteBatch);
                }
            }



            DrawRectangle(CollisionBox, spriteBatch);
        }

        private void UpdatePixelCollider()
        {
            if (loaded)
            {
                for (int i = 0; i < rectangles.Value.Count; i++)
                {
                    rectangles.Value[i].UpdatePosition(GameObject, spriteRenderer.Sprite.Width, spriteRenderer.Sprite.Height);
                }
            }

        }

        private void DrawRectangle(Rectangle collisionBox, SpriteBatch spriteBatch)
        {
            Rectangle topLine = new Rectangle(collisionBox.X, collisionBox.Y, collisionBox.Width, 1);
            Rectangle bottomLine = new Rectangle(collisionBox.X, collisionBox.Y + collisionBox.Height, collisionBox.Width, 1);
            Rectangle rightLine = new Rectangle(collisionBox.X + collisionBox.Width, collisionBox.Y, 1, collisionBox.Height);
            Rectangle leftLine = new Rectangle(collisionBox.X, collisionBox.Y, 1, collisionBox.Height);

            spriteBatch.Draw(texture, topLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(texture, bottomLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(texture, rightLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
            spriteBatch.Draw(texture, leftLine, null, Color.Red, 0, Vector2.Zero, SpriteEffects.None, 1);
        }

        private void CheckCollision()
        {
            foreach (Collider other in GameWorld.Instance.Colliders)
            {
                if (other != this && other.CollisionBox.Intersects(CollisionBox))
                {
                    foreach (RectangleData myRectangleData in rectangles.Value)
                    {
                        foreach (RectangleData otherRectangleData in other.rectangles.Value)
                        {
                            if (myRectangleData.Rectangle.Intersects(otherRectangleData.Rectangle))
                            {
                                CollisionEvent.Notify(other.GameObject);
                            }
                        }
                    }
                }
            }
        }
    }

    public class RectangleData
    {
        public Rectangle Rectangle { get; set; }

        public int X { get; set; }

        public int Y { get; set; }

        public RectangleData(int x, int y)
        {
            this.Rectangle = new Rectangle();
            this.X = x;
            this.Y = y;
        }

        public void UpdatePosition(GameObject gameObject, int width, int height)
        {
            Rectangle = new Rectangle((int)gameObject.Transform.Position.X + X - width / 2, (int)gameObject.Transform.Position.Y + Y - height / 2, 1, 1);
        }
    }
}


