using System;
using System.Collections.Generic;
using System.Text;

namespace StrategyGame2
{
    class WorkerFactory : Factory
    {
        private static WorkerFactory instance;

        public static WorkerFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new WorkerFactory();
                }
                return instance;
            }
        }


        public override GameObject Create(object generic)
        {
            GameObject gameObject = new GameObject();

            Worker worker = (Worker)gameObject.AddComponent(new Worker());

            gameObject.AddComponent(new SpriteRenderer());
            Collider c = (Collider)gameObject.AddComponent(new Collider());
            c.CollisionEvent.Attach(worker);

            return gameObject;
        }
    }
}
