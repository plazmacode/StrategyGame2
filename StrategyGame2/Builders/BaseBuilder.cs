using System;
using System.Collections.Generic;
using System.Text;

namespace StrategyGame2
{
    /// <summary>
    /// OBSOLETE
    /// Class to create a single base. Easier to pass parameters to BaseFactory
    /// </summary>
    class BaseBuilder : IBuilder
    {
        private GameObject gameObject;
        public Player Owner { get; set; }

        public void BuildGameObject()
        {
            gameObject = new GameObject();

            BuildComponents();
        }

        private void BuildComponents()
        {
            string randomPlayerKey = PlayerManager.Instance.PlayerKeys[GameWorld._Random.Next(0, PlayerManager.Instance.PlayerKeys.Count)];
            Owner = PlayerManager.Instance.Players[randomPlayerKey];

            BaseBuilding baseBuilding = (BaseBuilding)gameObject.AddComponent(new BaseBuilding());
            baseBuilding.Owner = Owner;

            SpriteRenderer sr = new SpriteRenderer();
            sr.Color = Owner.Color;

            gameObject.AddComponent(Owner);
            gameObject.AddComponent(sr);
            Collider c = (Collider)gameObject.AddComponent(new Collider());
            c.CollisionEvent.Attach(baseBuilding);
        }

        public GameObject GetResult()
        {
            return gameObject;
        }
    }
}
