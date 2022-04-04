using System;
using System.Collections.Generic;
using System.Text;

namespace StrategyGame2
{
    class WorldBuilder : IBuilder
    {
        private GameObject gameObject;
        public void BuildGameObject()
        {
            gameObject = new GameObject();

            BuildComponents();
        }

        private void BuildComponents()
        {
            World world = (World)gameObject.AddComponent(World.Instance);
            //world.AddComponent(new Component());
        }

        public GameObject GetResult()
        {
            return gameObject;
        }
    }
}
