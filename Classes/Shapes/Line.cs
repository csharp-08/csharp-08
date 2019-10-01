using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;

namespace csharp_08
{
    public class Line : Shape
    {
        public Line(List<Tuple<double, double>> Vertices, User Owner, int Thickness = 1, Color Color = new Color()) :
               base(Vertices, Owner, Thickness, Color)
        {

        }

        public override string ToString()
        {
            return "Forme: Ligne\n" + base.ToString();
        }
    }
}
