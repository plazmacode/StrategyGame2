using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace StrategyGame2
{
    class Worker : Component, IGameListener
    {
        public Player Owner { get; set; }

        private float speed = 2;

        private Vector2 velocity;

        public override void Start()
        {
            SpriteRenderer sr = GameObject.GetComponent<SpriteRenderer>() as SpriteRenderer;
            sr.SetSprite("worker");
            sr.Layer = 0.9f;
            velocity = new Vector2(1, 0);
        }

        public override void Update()
        {
            //GameObject.Transform.Translate(velocity * speed * GameWorld.DeltaTime);
        }

        public void Notify(GameEvent gameEvent)
        {

        }
    }
}
