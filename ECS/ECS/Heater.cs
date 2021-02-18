using System;

namespace ECS
{
    public class Heater : IHeater
    {
        public void TurnOn()
        {
            Console.WriteLine("Heater is on");
        }
        
        public void TurnOff()
        {
            Console.WriteLine("Heater is off");
        }

        public bool RunSelfTest()
        {
            return true;
        }


    }
}