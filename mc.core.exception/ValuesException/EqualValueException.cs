using System;
using System.Collections.Generic;
using System.Text;

namespace mc.core.exception.ValuesException
{
    public class EqualValueException: Exception
    {
        public EqualValueException()
            : base($"There is a value with the same caracteristcs.")
        {

        }
    }
}
