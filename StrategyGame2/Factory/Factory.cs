using System;
using System.Collections.Generic;
using System.Text;

namespace StrategyGame2
{
    public abstract class Factory
    {
        public abstract GameObject Create(object generic);
    }
}
