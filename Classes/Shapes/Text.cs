using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace csharp_08
{
    public class Text : Shape
    {
        public string InnerText;
        public uint FontSize { get; private set; }

        public Text(List<Tuple<double, double>> Vertices, User Owner, string InnerText, uint FontSize, ShapeConfig Config, uint ID) :
               base(Vertices, Owner, Config, ID)
        {
            this.InnerText = InnerText;
            this.FontSize = FontSize;
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
