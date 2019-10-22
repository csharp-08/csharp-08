using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace csharp_08
{
    public class Pencil : Shape
    {
        public Pencil(List<Tuple<double, double>> Vertices, User Owner, ShapeConfig Config, uint ID) : base(Vertices, Owner, Config, ID)
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

        public override byte GetShapeCode()
        {
            return (byte)ShapeCode.Pencil;
        }
    }
}
