using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;

namespace csharp_08
{
    public abstract class Shape
    {
        private static uint IDs = 0;
        public uint ID { get; private set; }
        public List<Tuple<double, double>> Vertices { get; private set; }
        public ShapeConfig Config { get; private set; }
        public User Owner { get; set; }

        public Shape(List<Tuple<double, double>> Vertices, User Owner = null,
                    int Thickness = 1, Color BorderColor = new Color(), Color Color = new Color(),
                    double OffsetX = 0, double OffsetY = 0, double ScaleX = 0, 
                    double ScaleY = 0, double Rotate = 0, bool IsEmpty = true)
        {
            ID = IDs;
            IDs++;

            Config = new ShapeConfig(Thickness, Color, BorderColor, OffsetX, OffsetY, ScaleX, ScaleY, Rotate, IsEmpty);

            this.Vertices = Vertices;
            this.Owner = Owner;
        }

        public override string ToString()
        {
            string str = "";

            str += "ID: " + ID;
            str += "\nPropriétaire: " + Owner.Username;
            str += "\nListe des points:";
            foreach (Tuple<double, double> point in Vertices)
            {
                str += "\n\tx: " + point.Item1 + ", y: " + point.Item2;
            }
            str += "\nÉpaisseur: " + Config.Thickness;
            str += "\nCouleur: " + Config.Color.ToString();

            return str;
        }

        public abstract string Draw();

    }
}
