using System;
using System.Collections.Generic;
using System.Text;

namespace ECS
{
    public interface IWindow
    {
        void open();
        void close();
        bool self_test();
    }
}
