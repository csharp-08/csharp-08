using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace csharp_08
{
    public abstract class Shape
    {
        private static uint IDs = 1;
        public uint ID { get; private set; }
        public List<Tuple<double, double>> Vertices { get; private set; }
        public ShapeConfig Config { get; private set; }
        public User Owner { get; set; }

        protected Shape(List<Tuple<double, double>> Vertices, User Owner = null, ShapeConfig Config = null, uint ID = 0)
        {
            if (ID == 0)
            {
                Debug.WriteLine("AAAAAA");
                this.ID = IDs;
                IDs++;
            } else
            {
                Debug.WriteLine("BBBBBB");
                this.ID = ID;
            }

            this.Vertices = Vertices;
            this.Owner = Owner;
            if (Config == null)
            {
                this.Config = ShapeConfig.DefaultConfig();
            } else
            {
                this.Config = Config;
            }
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

        public void UpdateWithNewShape(Shape updatedShape)
        {
            if (this.ID != updatedShape.ID)
            {
                return;
            }

            this.Vertices = updatedShape.Vertices;
            this.Config = updatedShape.Config;
        }
    }
}
