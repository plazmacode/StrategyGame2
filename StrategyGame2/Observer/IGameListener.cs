using System;
using System.Collections.Generic;
using System.Text;

namespace TugOfWar
{
    public interface IGameListener
    {
        void Notify(GameEvent gameEvent);
    }
}
