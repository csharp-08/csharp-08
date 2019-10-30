using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace csharp_08
{
    public class Polygon : Shape
    {
        public Polygon(List<Tuple<double, double>> Vertices, User Owner, ShapeConfig Config, uint ID) : base(Vertices, Owner, Config, ID)
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

        public override byte GetShapeCode()
        {
            return (byte)ShapeCode.Polygon;
        }
    }
}