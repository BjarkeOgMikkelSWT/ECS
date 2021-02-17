namespace ECS
{
    public class Heater : IHeater
    {
        private readonly ConsoleOutputter _outputter = new ConsoleOutputter();
        
        public void TurnOn()
        {
            _outputter.WriteLine("Heater is on");
        }
        
        public void TurnOff()
        {
            _outputter.WriteLine("Heater is off");
        }

        public bool RunSelfTest()
        {
            return true;
        }


    }
}