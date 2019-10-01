using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;

namespace csharp_08
{
    public class Polygon : Shape
    {
        public Polygon(List<Tuple<double, double>> Vertices, User Owner, int Thickness = 1, Color Color = new Color()) : 
               base(Vertices, Owner, Thickness, Color)
        {
            if (Vertices.Count() < 3)
            {
                throw new InvalidShapeDefinitionException("Polygon", "A Polygone must be defined by more than 3 points");
            }
        }

        public override string ToString()
        {
            return "Forme: Polygone\n" + base.ToString();
        }
    }
}
