using System;
using System.Collections.Generic;
using System.Globalization;
using csharp_08.Utils;

namespace csharp_08
{
    /// <summary>
    /// Class that describes a Circle object from the writeboard.
    /// This class is heriting from the class Shape.
    /// </summary>
    public class Circle : Shape
    {
        // Circle's Radius. Needs to be public for string to shape operations
        public double Radius { get; private set; }

        /// <summary>
        /// Constructor of the Circle object
        /// </summary>
        /// <param name="Vertices">The coordinates of the points that describe the center of the circle</param>
        /// <param name="Owner">The user owning the Circle Object</param>
        /// <param name="Radius">The radius of the circle</param>
        /// <param name="Config">The graphical configuration of the circle</param>
        /// <param name="ID">The unique id of the shape</param>
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

        public override byte GetShapeCode()
        {
            return (byte)ShapeCode.Circle;
        }

        public override string ToSVG()
        {
            return String.Format("<circle cx = \"{0}\" cy = \"{1}\" r = \"{2}\" stroke = \"{3}\" fill = \"{4}\" stroke-width = \"{5}\" transform = \"rotate({6}) translate({7} {8}) scale({9} {10})\"/>",
                                Vertices[0].Item1,
                                Vertices[0].Item2,
                                Radius.ToString(CultureInfo.InvariantCulture.NumberFormat),
                                ShapeUtils.ColorToHex(Config.BorderColor),
                                Config.Color.IsEmpty ? "#00000000" : ShapeUtils.ColorToHex(Config.Color),
                                Config.Thickness,
                                Config.Rotation,
                                Config.OffsetX,
                                Config.OffsetY,
                                Config.ScaleX == 0 ? 1 : Config.ScaleX,
                                Config.ScaleY == 0 ? 1 : Config.ScaleY);
        }
    }
}