﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;

namespace csharp_08
{
    public class Line : Shape
    {
        public Line(List<Tuple<double, double>> ShapeVertices, User User, int ShapeThickness = 1, Color ShapeColor = new Color()) :
               base(ShapeVertices, User, ShapeThickness, ShapeColor)
        {

        }
    }
}
