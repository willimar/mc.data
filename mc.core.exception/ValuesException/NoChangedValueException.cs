using System;
using System.Collections.Generic;
using System.Text;

namespace mc.core.exception.ValuesException
{
    public class NoChangedValueException : Exception
    {
        public NoChangedValueException()
            : base($"No changed value.")
        {

        }
    }
}
