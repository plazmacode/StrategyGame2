using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace StrategyGame2
{
    public abstract class Component
    {
        public bool IsEnabled { get; set; } = true;
        public GameObject GameObject { get; set; }

        public virtual void Awake()
        {

        }

        public virtual void Start()
        {

        }

        public virtual void Update()
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }

        public virtual object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
