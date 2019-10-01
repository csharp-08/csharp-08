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
        public static uint ID { get; private set; }
        public static List<Tuple<double, double>> Vertices { get; private set; }
        public static int Thickness { get; private set; }
        public static Color Color { get; private set; }
        public static User Owner { get; private set; }
        
        public Shape(List<Tuple<double, double>> ShapeVertices, User User, int ShapeThickness = 1, Color ShapeColor = new Color())
        {
            ID = IDs;
            IDs++;

            Vertices = ShapeVertices;
            Thickness = ShapeThickness;
            Color = ShapeColor;
            Owner = User;
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
