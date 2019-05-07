using System;
using System.Collections.Generic;
using System.Text;

namespace mc.core.exception.ValuesException
{
    public class InvalidValueException : Exception
    {
        public InvalidValueException(string valueName) 
            : base($"Invalid value to {valueName}")
        {

        }
    }
}
