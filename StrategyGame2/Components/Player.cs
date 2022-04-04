using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace StrategyGame2
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
            BaseBuilding b = GameObject.GetComponent<BaseBuilding>() as BaseBuilding;
            b.SpawnWorkers();
        }
    }
}
