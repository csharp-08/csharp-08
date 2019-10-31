﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace csharp_08
{
    public class Point : Shape
    {
        public Point(List<Tuple<double, double>> Vertices, User Owner, ShapeConfig Config, uint ID) : base(Vertices, Owner, Config, ID)
        {
            this.Code = ShapeCode.Point;
        }

        public override string ToString()
        {
            return "Forme: Point" + Environment.NewLine + base.ToString();
        }

        public override string Draw()
        {
            return JsonConvert.SerializeObject(this);
        }

        public override byte GetShapeCode()
        {
            return (byte)ShapeCode.Point;
        }
    }
}