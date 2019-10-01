using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace csharp_08
{
    public class NegativeRadiusException : Exception
    {
        public NegativeRadiusException() : base("Radius must be a positive float")
        {

        }
    }
}
