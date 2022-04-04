﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace StrategyGame2
{
    class MoveCameraCommand : ICommand
    {
        private Vector2 velocity;

        public MoveCameraCommand(Vector2 velocity)
        {
            this.velocity = velocity;
        }
        public void Execute()
        {
            GameWorld.Instance.Camera.Move(velocity);
        }
    }
}
