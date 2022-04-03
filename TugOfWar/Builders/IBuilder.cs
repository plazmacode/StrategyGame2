using System;
using System.Collections.Generic;
using System.Text;

namespace TugOfWar
{
    interface IBuilder
    {
        void BuildGameObject();

        GameObject GetResult();
    }
}
