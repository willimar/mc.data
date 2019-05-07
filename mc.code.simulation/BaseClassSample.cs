using System;
using System.Collections.Generic;
using System.Text;

namespace mc.code.simulation
{
    internal class BaseClassSample
    {
        public virtual void PrintLine(string value)
        {
            Console.WriteLine($"Value in base class is: {value}");
        }
    }
}
