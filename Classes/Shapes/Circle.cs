using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace csharp_08
{
    public class Circle : Shape
    {
        public double Radius { get; private set; }

        public Circle(List<Tuple<double, double>> Vertices, User Owner, double Radius, ShapeConfig Config, uint ID) : base(Vertices, Owner, Config, ID)
        {
            if (Radius < 0)
                throw new NegativeRadiusException();

            this.Radius = Radius;
            this.Code = ShapeCode.Circle;
        }

        public override string ToString()
        {
            string str = "";

            str += "ID: " + ID;
            str += "\nPropriétaire: " + Owner.Username;
            str += "\nCentre:";

            Tuple<double, double> point = Vertices[0];
            str += "\n\tx: " + point.Item1 + ", y: " + point.Item2;

            str += "\nRayon: " + Radius;
            str += "\nÉpaisseur: " + Config.Thickness;
            str += "\nCouleur: " + Config.Color.ToString();

            return str;
        }

        public override string Draw()
        {
            return JsonConvert.SerializeObject(this);
        }

        public override byte GetShapeCode()
        {
            return (byte)ShapeCode.Circle;
        }
    }
}