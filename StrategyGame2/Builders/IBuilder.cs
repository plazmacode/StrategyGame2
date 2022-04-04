using System;
using System.Collections.Generic;
using System.Text;

namespace StrategyGame2
{
    interface IBuilder
    {
        void BuildGameObject();

        GameObject GetResult();
    }
}
