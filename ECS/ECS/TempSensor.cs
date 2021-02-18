using System;

namespace ECS
{
    public class TempSensor : ITempSensor
    {
        private readonly Random _gen = new Random();

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