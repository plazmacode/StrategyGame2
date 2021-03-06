using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace StrategyGame2
{
    class BaseBuilding : Component, IGameListener
    {
        private SpriteRenderer spriteRenderer;

        public Player Owner { get; set; }

        public void Notify(GameEvent gameEvent)
        {

        }

        public override void Start()
        {
            spriteRenderer = GameObject.GetComponent<SpriteRenderer>() as SpriteRenderer;
            spriteRenderer.SetSprite("base");
            spriteRenderer.Layer = 0.5f;
            int x = GameWorld._Random.Next(0, (int)(World.Instance.WorldSize.X / World.Instance.Grid.GridSize) -1);
            int y = GameWorld._Random.Next(0, (int)(World.Instance.WorldSize.Y / World.Instance.Grid.GridSize) - 1);

            //Use this spawnOffset to put object inside grid correctly. Maybe make it a method on the spriteRenderer
            //Make generic method to place objects in grid. There it will check if object size goes beyond grid
            Vector2 spawnOffset = new Vector2(spriteRenderer.Sprite.Width / 2, spriteRenderer.Sprite.Height / 2);

            GameObject.Transform.Position = World.Instance.Grid.Cells[new Vector2(x, y)].GameObject.Transform.Position + spawnOffset;
        }
        public void SpawnWorkers()
        {
            GameObject gameObject = WorkerPool.Instance.GetObject();
            Worker w = gameObject.GetComponent<Worker>() as Worker;
            SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>() as SpriteRenderer;

            w.Owner = Owner;
            sr.Color = Owner.Color;

            gameObject.Transform.Position = GameObject.Transform.Position;

            GameWorld.Instance.Instantiate(gameObject);
        }

    }
}
