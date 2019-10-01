﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;

namespace csharp_08
{
    public class Point : Shape
    {
        public Point(List<Tuple<double, double>> Vertices, User Owner, int Thickness = 1, Color Color = new Color()) :
               base(Vertices, Owner, Thickness, Color)
        {
            if (Vertices.Count() != 1)
            {
                throw new InvalidShapeDefinitionException("Point");
            }
        }
        public override string ToString()
        {
            return "Forme: Point\n" + base.ToString();
        }
    }
}
