using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;
using Newtonsoft.Json;

namespace csharp_08.Classes.Shapes
{
    public class Text : Shape
    {
        public string InnerText;
        public Text(List<Tuple<double, double>> Vertices, User Owner, string Text, int Thickness = 1, Color Color = new Color()) :
               base(Vertices, Owner, Thickness, Color)
        {
            InnerText = Text;
        }

        public override string ToString()
        {
            return "Forme: Text\nText: " + InnerText + "\n" + base.ToString();
        }

        public override string Draw()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
