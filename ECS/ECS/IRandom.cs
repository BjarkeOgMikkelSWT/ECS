using System;
using System.Collections.Generic;
using System.Text;

namespace ECS
{
    public interface IRandom
    {
        public int Next(int min, int max);
    }
}
