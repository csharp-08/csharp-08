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
        public double Radius { get; private set; }
        public Circle(List<Tuple<double, double>> Vertices, User Owner, double Radius, 
                    int Thickness = 1, Color BorderColor = new Color(), Color Color = new Color(),
                    double OffsetX = 0, double OffsetY = 0, double ScaleX = 0,
                    double ScaleY = 0, double Rotate = 0, bool IsEmpty = true) :
               base(Vertices, Owner, Thickness, BorderColor, Color, OffsetX, OffsetY, ScaleX, ScaleY, Rotate, IsEmpty)
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
            str += "\nÉpaisseur: " + Config.Thickness;
            str += "\nCouleur: " + Config.Color.ToString();

            return str;
        }

        public override string Draw()
        {
            return JsonConvert.SerializeObject(this);
        }

    }
}
