using System;
using System.Collections.Generic;
using System.Text;

namespace mc.code.simulation
{
    internal class TopClassSample: BaseClassSample
    {
        public override void PrintLine(string value)
        {
            base.PrintLine(value);
            Console.WriteLine($"Value in top class is: {value}");
        }
    }
}
