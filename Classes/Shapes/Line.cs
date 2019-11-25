using csharp_08.Utils;
using System;
using System.Collections.Generic;

namespace csharp_08
{
    /// <summary>
    /// Class that describes a STRAIGHT line object from the writeboard.
    /// This class is heriting from the class Shape.
    /// </summary>
    public class Line : Shape
    {
        /// <summary>
        /// Constructor of the Line object
        /// </summary>
        /// <param name="Vertices">The coordinates of the points that describe the line</param>
        /// <param name="Owner">The user owning the line</param>
        /// <param name="Config">The graphical configuration of the line</param>
        /// <param name="ID">The unique id of the shape</param>
        public Line(List<Tuple<double, double>> Vertices, User Owner, ShapeConfig Config, uint ID) : base(Vertices, Owner, Config, ID)
        {
            this.Code = ShapeCode.Line;
        }

        public override string ToString()
        {
            return "Forme: Ligne\n" + base.ToString();
        }

        public override byte GetShapeCode()
        {
            return (byte)ShapeCode.Line;
        }

        public override string ToSVG()
        {
            return String.Format("<line x1 = \"{0}\" x2 = \"{1}\" y1 = \"{2}\" y2 = \"{3}\" stroke = \"{4}\" fill = \"{5}\" stroke-width = \"{6}\" transform = \"rotate({7}) translate({8} {9}) scale({10} {11})\"/>",
                                Vertices[0].Item1,
                                Vertices[1].Item1,
                                Vertices[0].Item2,
                                Vertices[1].Item2,
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