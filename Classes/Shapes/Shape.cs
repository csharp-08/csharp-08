using System;
using System.Collections.Generic;

namespace csharp_08
{
    public abstract class Shape
    {
        private static uint IDs = 0;
        public uint ID { get; private set; }
        public List<Tuple<double, double>> Vertices { get; private set; }
        public ShapeConfig Config { get; private set; }
        public User Owner { get; set; }
        public byte OverrideUserPolicy { get; set; }
        public ShapeCode Code;

        protected Shape(List<Tuple<double, double>> Vertices, User Owner = null, ShapeConfig Config = null, uint ID = 0)
        {
            if (ID == 0)
            {
                this.ID = IDs;
                IDs++;
            }
            else
            {
                this.ID = ID;
            }

            this.Vertices = Vertices;
            this.Owner = Owner;
            if (Config == null)
            {
                this.Config = ShapeConfig.DefaultConfig;
            }
            else
            {
                this.Config = Config;
            }

            this.Code = ShapeCode.Pencil;
            this.OverrideUserPolicy = 0;
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
            str += "\nConfig: " + Config;

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

        public abstract byte GetShapeCode();

        public static void UpdateStartPoint(uint StartPoint)
        {
            IDs = StartPoint > IDs ? StartPoint : IDs;
        }
    }
}