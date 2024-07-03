using System;
using System.Collections.Generic;
using System.Text;

/*
 Author : Theodorus Danang Wicaksono
 */
namespace NumberToWords
{
    public class MaxDigitException : Exception
    {
        public MaxDigitException(string message) : base(
            String.Format("The maximum limit of whole number is {0} digits.", message))
        {}
    }

    public class DecimalException : Exception
    {
        public DecimalException(string message) : base(
            String.Format("Your input is not valid decimal currency value!"))
        { }
    }
}
