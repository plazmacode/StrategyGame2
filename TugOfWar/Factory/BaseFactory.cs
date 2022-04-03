using System;
using System.Collections.Generic;
using System.Text;

namespace TugOfWar
{
    class BaseFactory : Factory
    {
        private static BaseFactory instance;
        public static BaseFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new BaseFactory();

                }
                return instance;
            }
        }
        public override GameObject Create(object playerObject)
        {
            GameObject gameObject = new GameObject();

            BaseBuilding baseBuilding = (BaseBuilding)gameObject.AddComponent(new BaseBuilding());

            SpriteRenderer sr = new SpriteRenderer();
            Player player = (Player)playerObject;

            sr.Color = player.Color;

            gameObject.AddComponent(player);
            gameObject.AddComponent(sr);
            Collider c = (Collider)gameObject.AddComponent(new Collider());
            c.CollisionEvent.Attach(baseBuilding);

            return gameObject;
        }
    }
}
