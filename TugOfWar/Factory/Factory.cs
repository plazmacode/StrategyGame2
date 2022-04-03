using System;
using System.Collections.Generic;
using System.Text;

namespace TugOfWar
{
    public abstract class Factory
    {
        public abstract GameObject Create(object generic);
    }
}
