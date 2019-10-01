using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;
using Newtonsoft.Json;

namespace csharp_08
{
    public class Pencil : Shape
    {
        public Pencil(List<Tuple<double, double>> Vertices, User Owner, int Thickness = 1, Color Color = new Color()) :
               base(Vertices, Owner, Thickness, Color)
        {

        }

        public override string ToString()
        {
            return "Forme: Forme Libre\n" + base.ToString();
        }

        public override string Draw()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
