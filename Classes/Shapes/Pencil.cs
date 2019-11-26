using csharp_08.Utils;
using System;
using System.Collections.Generic;

namespace csharp_08
{
    /// <summary>
    /// Class that describes a pencil stroke object from the writeboard.
    /// This class is heriting from the class Shape.
    /// </summary>
    public class Pencil : Shape
    {
        /// <summary>
        /// Constructor of the Pencil object
        /// </summary>
        /// <param name="Vertices">The coordinates of the points that describe the pencil stroke</param>
        /// <param name="Owner">The user owning the pencil stroke</param>
        /// <param name="Config">The graphical configuration of the pencil stroke</param>
        /// <param name="ID">The unique id of the shape</param>
        public Pencil(List<Tuple<double, double>> Vertices, User Owner, ShapeConfig Config, uint ID) : base(Vertices, Owner, Config, ID)
        {
            this.Code = ShapeCode.Pencil;
        }

        public override string ToString()
        {
            return "Forme: Forme Libre" + Environment.NewLine + base.ToString();
        }

        public override byte GetShapeCode()
        {
            return (byte)ShapeCode.Pencil;
        }

        public override string ToSVG()
        {
            string polyline = "";

            foreach (Tuple<double, double> point in Vertices)
            {
                polyline += String.Format("{0} {1}", point.Item1, point.Item2);
            }

            return String.Format("<polyline points=\"{0}\" stroke = \"{1}\" fill = \"{2}\" stroke-width = \"{3}\" transform = \"rotate({4}) translate({5} {6}) scale({7} {8})\"/>",
                                polyline,
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