using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace TugOfWar
{
    class Player : Component
    {
        public bool AI { get; set; } = false;

        public Color Color { get; set; }

        public string Name { get; set; }

        public Player(string name, Color color)
        {
            Name = name;
            Color = color;
        }

        public override void Start()
        {
            
        }

        public override void Update()
        {
            if (World.Tick)
            {
                TickPlayer();
            }
        }

        public void TickPlayer()
        {
            GameObject.Transform.Position = new Vector2(GameObject.Transform.Position.X + 5, GameObject.Transform.Position.Y);
        }
    }
}
