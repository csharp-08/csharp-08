using csharp_08.Utils;
using System;
using System.Collections.Generic;

namespace csharp_08
{
    /// <summary>
    /// Class that describes a polygon object from the writeboard.
    /// This class is heriting from the class Shape.
    /// </summary>
    public class Polygon : Shape
    {
        /// <summary>
        /// Constructor of the Polygon object
        /// </summary>
        /// <param name="Vertices">The coordinates of the points that describe the polygon</param>
        /// <param name="Owner">The user owning the polygon</param>
        /// <param name="Config">The graphical configuration of the polygon</param>
        /// <param name="ID">The unique id of the shape</param>
        public Polygon(List<Tuple<double, double>> Vertices, User Owner, ShapeConfig Config, uint ID) : base(Vertices, Owner, Config, ID)
        {
            this.Code = ShapeCode.Polygon;
        }

        public override string ToString()
        {
            return "Forme: Polygone" + Environment.NewLine + base.ToString();
        }

        public override byte GetShapeCode()
        {
            return (byte)ShapeCode.Polygon;
        }

        public override string ToSVG()
        {
            string polygon = "";

            foreach (Tuple<double, double> point in Vertices)
            {
                polygon += String.Format("{0} {1}", point.Item1, point.Item2);
            }

            return String.Format("<polygon points=\"{0}\" stroke = \"{1}\" fill = \"{2}\" stroke-width = \"{3}\" transform = \"rotate({4}) translate({5} {6}) scale({7} {8})\"/>",
                                polygon,
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