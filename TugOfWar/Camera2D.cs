using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace TugOfWar
{
    public class Camera2D
    {
        public float Zoom { get; set; }
        public float InvertZoom { get; set; } //Used for renderRect
        public Matrix Transform { get; set; }
        public Vector2 Position { get; set; }
        public Vector2 Origin { get; set; }
        public float Rotation { get; set; }

        private float speed;

        public bool CameraChanged { get; set; } = true;

        public Rectangle RenderRect { get; set; }

        public Camera2D()
        {
            speed = 1.2f;
            Zoom = 1.0f;
            InvertZoom = 1.0f;
            Rotation = 0.0f;
            Position = Vector2.Zero;
        }

        /// <summary>
        /// Sets origin of camera. This must be done later than camera creation because GraphicsDevice will be null at start, causing overflowException
        /// </summary>
        public void SetOrigin()
        {
            Origin = new Vector2(GameWorld.Instance.GraphicsDevice.Viewport.Width / 2, GameWorld.Instance.GraphicsDevice.Viewport.Height / 2);

        }

        public void OnResize()
        {
            SetOrigin();
            CameraChanged = true;
        }

        private float maxZoom = 2.0f;
        private float minZoom = 0.2f;

        public void ZoomCamera(float amount)
        {
            if (Zoom * amount > minZoom && Zoom * amount < maxZoom)
            {
                Zoom *= amount;
                InvertZoom /= amount;
            }
            if (Zoom < minZoom)
            {
                Zoom = minZoom;
            }
            if (Zoom > maxZoom)
            {
                Zoom = maxZoom;
            }
            CameraChanged = true;
        }

        public void Move(Vector2 velocity)
        {
            //CameraChanged = true;
            //Vector2 oldPosition = Position;
            Position += (velocity * speed * GameWorld.DeltaTime * InvertZoom);
            if (Position.X < 0)
            {
                Position = new Vector2(0, Position.Y);
            }
            if (Position.X > World.Instance.WorldSize.X)
            {
                Position = new Vector2(World.Instance.WorldSize.X, Position.Y);
            }
            if (Position.Y < 0)
            {
                Position = new Vector2(Position.X, 0);
            }
            if (Position.Y > World.Instance.WorldSize.Y)
            {
                Position = new Vector2(Position.X, World.Instance.WorldSize.Y);
            }
        }

        public bool ViewAble(Vector2 obj)
        {
            bool viewAble = false;
            Vector2 renderSize = new Vector2(GameWorld.Instance.GraphicsDevice.Viewport.Width, GameWorld.Instance.GraphicsDevice.Viewport.Height);
            RenderRect = new Rectangle(
                (int)Position.X * (int)Zoom - (int)renderSize.X / 2,
                (int)Position.Y * (int)Zoom - (int)renderSize.Y / 2,
                (int)renderSize.X, (int)renderSize.Y);
            //RenderRect = new Rectangle((int)Position.X - 400, (int)Position.Y - 400, 800, 800); //Small renderRect
            if (RenderRect.Contains(obj))
            {
                viewAble = true;
            }
            return viewAble;
        }

        public void UpdateRenderRect()
        {
            Vector2 renderSize = new Vector2(
                GameWorld.Instance.GraphicsDevice.Viewport.Width * InvertZoom,
                GameWorld.Instance.GraphicsDevice.Viewport.Height * InvertZoom);

            RenderRect = new Rectangle(
                (int)Position.X - (int)renderSize.X / 2,
                (int)Position.Y - (int)renderSize.Y / 2,
                (int)renderSize.X, (int)renderSize.Y);
            //RenderRect = new Rectangle((int)Position.X - 400, (int)Position.Y - 400, 800, 800); //Small renderRect
        }

        public bool ViewAble(Rectangle rect)
        {
            bool viewAble = false;
            
            if (RenderRect.Intersects(rect))
            {
                viewAble = true;
            }
            return viewAble;
        }

        public Matrix GetTransformation(GraphicsDevice graphicsDevice) {
            Transform =
                Matrix.CreateTranslation(new Vector3(-Position.X, -Position.Y, 0)) *
                Matrix.CreateRotationZ(Rotation) *
                Matrix.CreateScale(new Vector3(Zoom, Zoom, 1)) *
                Matrix.CreateTranslation(new Vector3(Origin.X, Origin.Y, 0));
            return Transform;
        }
    }
}
