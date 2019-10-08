using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;
using Newtonsoft.Json;

namespace csharp_08
{
    public class Line : Shape
    {
        public Line(List<Tuple<double, double>> Vertices, User Owner,
                    int Thickness = 1, Color BorderColor = new Color(), Color Color = new Color(),
                    double OffsetX = 0, double OffsetY = 0, double ScaleX = 0,
                    double ScaleY = 0, double Rotate = 0, bool IsEmpty = true) :
               base(Vertices, Owner, Thickness, BorderColor, Color, OffsetX, OffsetY, ScaleX, ScaleY, Rotate, IsEmpty)
        {

        }

        public override string ToString()
        {
            return "Forme: Ligne\n" + base.ToString();
        }

        public override string Draw()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
