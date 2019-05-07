using System;
using System.Collections.Generic;
using System.Text;

namespace mc.core.exception.ValuesException
{
    public class ValueNotFoundException: Exception
    {
        public ValueNotFoundException()
            : base($"No values found.")
        {

        }
    }
}
