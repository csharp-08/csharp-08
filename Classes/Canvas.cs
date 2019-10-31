using System.Collections.Generic;

namespace csharp_08
{
    public class Canvas
    {
        public Dictionary<uint, Shape> Shapes { get; private set; }
        private static uint IdCounter = 0;
        public uint Id { get; private set; }

        public Canvas()
        {
            this.Shapes = new Dictionary<uint, Shape>();
            Id = IdCounter;
            IdCounter++;
        }
    }
}