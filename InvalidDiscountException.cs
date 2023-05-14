using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Price_Calculator_kata
{
    namespace Price_Calculator_kata
    {
        [Serializable]
        public class InvalidDiscountException : Exception
        {
            public static string DefaultMessage = "Invalid Discount! Discount percentage should be equal larger than 0 and smaller than 100";
            public InvalidDiscountException()
                : base(DefaultMessage) { }

            public InvalidDiscountException(string message)
                  : base(message) { }
            public InvalidDiscountException(string message, Exception inner)
                  : base(message, inner) { }
        }
    }
}
