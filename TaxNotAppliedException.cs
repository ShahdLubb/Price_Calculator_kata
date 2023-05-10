using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Price_Calculator_kata
{
    [Serializable]
    public class TaxNotAppliedException : Exception
    {
        public static string DefaultMessage = "Tax is mandotery, apply a Tax before trying to calculate the total price";
        public TaxNotAppliedException()
            : base(DefaultMessage) { }

        public TaxNotAppliedException(string message)
              : base(message) { }
        public TaxNotAppliedException(string message, Exception inner)
              : base(message, inner) { }
    }
}