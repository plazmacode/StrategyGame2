using System;
using System.Collections.Generic;
using System.Text;

namespace StrategyGame2
{
    public interface IGameListener
    {
        void Notify(GameEvent gameEvent);
    }
}
