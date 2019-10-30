using System;

namespace csharp_08
{
    public class NegativeRadiusException : Exception
    {
        public NegativeRadiusException() : base("Radius must be a positive float")
        {
        }
    }
}