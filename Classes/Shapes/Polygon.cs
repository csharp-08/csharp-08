using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;
using Newtonsoft.Json;

namespace csharp_08
{
    public class Polygon : Shape
    {
        public Polygon(List<Tuple<double, double>> Vertices, User Owner,
                    int Thickness = 1, Color BorderColor = new Color(), Color Color = new Color(),
                    double OffsetX = 0, double OffsetY = 0, double ScaleX = 0,
                    double ScaleY = 0, double Rotation= 0, bool IsEmpty = true) :
               base(Vertices, Owner, Thickness, BorderColor, Color, OffsetX, OffsetY, ScaleX, ScaleY, Rotation, IsEmpty)
        {

        }

        public override string ToString()
        {
            return "Forme: Polygone\n" + base.ToString();
        }
        public override string Draw()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
