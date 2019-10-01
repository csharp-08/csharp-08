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
        public int Thickness { get; private set; }
        public Color Color { get; private set; }
        public User Owner { get; private set; }
        
        public Shape(List<Tuple<double, double>> Vertices, User Owner, int Thickness = 1, Color Color = new Color())
        {
            ID = IDs;
            IDs++;

            this.Vertices = Vertices;
            this.Thickness = Thickness;
            this.Color = Color;
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
            str += "\nÉpaisseur: " + Thickness;
            str += "\nCouleur: " + Color.ToString();

            return str;
        }

    }
}
