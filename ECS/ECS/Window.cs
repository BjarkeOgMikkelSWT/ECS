using System;
using System.Collections.Generic;
using System.Text;

namespace ECS
{
    public class Window : IWindow
    {
        public void close()
        {
            Console.WriteLine("Window is closed");
        }

        public void open()
        {
            Console.WriteLine("Window is open");
        }

        public bool self_test()
        {
            return true;
        }
    }
}
