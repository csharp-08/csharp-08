﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace csharp_08
{
    public class Line : Shape
    {
        public Line(List<Tuple<double, double>> Vertices, User Owner, ShapeConfig Config, uint ID) : base(Vertices, Owner, Config, ID)
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

        public override byte GetShapeCode()
        {
            return (byte)ShapeCode.Line;
        }
    }
}