using System;
using System.Collections.Generic;
using System.Text;

namespace ECS
{
    public class ECS_Random : IRandom
    {
        private readonly Random _gen = new Random();

        public int Next(int min, int max)
        {
            return _gen.Next(min, max);
        }
    }
}
