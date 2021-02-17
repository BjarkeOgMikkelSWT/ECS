using System;
using System.Collections.Generic;
using System.Text;

namespace ECS
{
    interface IRandom
    {
        public int Next(int min, int max);
    }
}
