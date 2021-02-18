using System;
using System.Collections.Generic;
using System.Text;

namespace ECS
{
    public class EnCoSy_with_window : EnCoSy
    {
        private readonly IWindow _window;

        public int ThrWindow { get; private set; }

        public EnCoSy_with_window(int thrHeat, int thrWindow, ITempSensor tempSensor, IHeater heater, IWindow window) : base(thrHeat, tempSensor, heater)
        {
            _window = window;

            SetThreshold(thrHeat, thrWindow);
        }

        public void SetThreshold(int thrHeat, int thrWindow)
        {
            if (thrWindow <= thrHeat)
            {
                throw new ArgumentException("Window threshold must be greater than heater threshold!");
            }

            base.SetThreshold(thrHeat);
            ThrWindow = thrWindow;
        }

        public new int[] GetThreshold()
        {
            return new [] {base.GetThreshold(), ThrWindow};
        }

        public new void Regulate()
        {
            base.Regulate();
            if (base.GetCurTemp() > ThrWindow)
            {
                _window.open();
            }
            else
            {
                _window.close();
            }
        }

        public new bool RunSelfTest()
        {
            return base.RunSelfTest() && _window.self_test();
        }
    }
}
