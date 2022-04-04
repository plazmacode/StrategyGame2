using System;
using System.Collections.Generic;
using System.Text;

namespace StrategyGame2
{
    class CameraZoomCommand : ICommand
    {
        private float amount;
        public CameraZoomCommand(float amount)
        {
            this.amount = amount;
        }

        public void Execute()
        {
            if (GameWorld.DeltaTime > 1)
            {
                GameWorld.Instance.Camera.ZoomCamera(amount);
            }
        }
    }
}
