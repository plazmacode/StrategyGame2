using System;
using System.Collections.Generic;
using System.Text;

namespace TugOfWar
{
    public class GameEvent
    {
        private List<IGameListener> listners = new List<IGameListener>();

        public void Attach(IGameListener listner)
        {
            listners.Add(listner);
        }

        public void Detach(IGameListener listner)
        {
            listners.Remove(listner);
        }

        public void Notify()
        {
            foreach (IGameListener listner in listners)
            {
                listner.Notify(this);
            }
        }


    }
}
