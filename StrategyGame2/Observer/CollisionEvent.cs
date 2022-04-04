using System;
using System.Collections.Generic;
using System.Text;

namespace StrategyGame2
{
    public class CollisionEvent : GameEvent
    {
        public GameObject Other { get; set; }

        public void Notify(GameObject other)
        {
            this.Other = other;

            base.Notify();
        }
    }
}
