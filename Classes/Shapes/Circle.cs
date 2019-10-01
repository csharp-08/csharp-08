using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;
using Newtonsoft.Json;


namespace csharp_08
{
    public class Circle : Shape
    {
        public int Radius { get; private set; }
        public Circle(List<Tuple<double, double>> Vertices, User Owner, int Thickness = 1, Color Color = new Color(), int Radius = 0) :
               base(Vertices, Owner, Thickness, Color)
        {
            this.Radius = Radius;
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
            str += "\nÉpaisseur: " + Thickness;
            str += "\nCouleur: " + Color.ToString();

            return str;
        }

        public override string Draw()
        {
            return JsonConvert.SerializeObject(this);
        }

    }
}
