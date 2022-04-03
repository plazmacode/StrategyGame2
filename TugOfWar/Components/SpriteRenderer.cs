using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace TugOfWar
{
    class SpriteRenderer : Component
    {
        public int number = 0;

        public Texture2D Sprite { get; set; }

        public Color Color { get; set; }

        public float Scale { get; set; }

        public float Layer { get; set; }

        public Vector2 Origin { get; set; }

        public override void Awake()
        {
            Scale = 1.0f;
            //Color = Color.White;
        }

        public override void Start()
        {
        }

        public void SetSprite(string spriteName)
        {
            Sprite = GameWorld.Instance.Content.Load<Texture2D>(spriteName);
            Origin = new Vector2(Sprite.Width / 2, Sprite.Height / 2);

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Rectangle objRect = new Rectangle(
                (int)(GameObject.Transform.Position.X - Sprite.Width / 2 ),
                (int)(GameObject.Transform.Position.Y - Sprite.Height / 2),
                (int)(Sprite.Width * Scale),
                (int)(Sprite.Height * Scale));
            //objRect.Inflate(Scale, Scale);
            if (GameWorld.Instance.Camera.RenderRect.Intersects(objRect) || GameWorld.Instance.Camera.CameraChanged || 1 == 1)
            {
                spriteBatch.Draw(Sprite, GameObject.Transform.Position, null, Color, default, Origin, Scale, SpriteEffects.None, Layer);
            }
        }
    }
}