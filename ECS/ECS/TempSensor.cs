using System;

namespace ECS.Legacy
{
    internal class TempSensor : ITempSensor
    {
        private readonly IRandom _gen;

        TempSensor(IRandom gen)
        {
            _gen = gen;
        }

        public int GetTemp()
        {
            return _gen.Next(-5, 45);
        }

        public bool RunSelfTest()
        {
            return true;
        }
    }
}