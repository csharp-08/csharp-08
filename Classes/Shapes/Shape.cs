using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;

namespace csharp_08
{
    public abstract class Shape
    {
        private static uint IDs = 0;
        public static uint ID { get; private set; }
        public static List<double> Vertices { get; private set; }
        public static int Thickness { get; private set; }
        public static Color ShapeColor { get; private set; }
        public static User User { get; private set; }
        
        public Shape()
        {
            ID = IDs;
            IDs++;
        }
    }
}
