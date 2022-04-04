using System;
using System.Collections.Generic;
using System.Text;

namespace StrategyGame2
{
    class WorkerPool : Objectpool
    {

        private static WorkerPool instance;

        public static WorkerPool Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new WorkerPool();
                }

                return instance;
            }
        }

        private static Random rnd = new Random();

        protected override void CleanUp(GameObject gameObject)
        {

        }

        protected override GameObject CreateObject()
        {
            return WorkerFactory.Instance.Create(new Worker());
        }
    }
}
