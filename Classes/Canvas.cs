using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace csharp_08
{
    public class Canvas
    {
        public List<Shape> ShapeList { get; private set; }

        public Canvas()
        {
            this.ShapeList = new List<Shape>();
        }

        public void Add(Shape newShape)
        {
            this.ShapeList.Add(newShape);
        }
    }
}
