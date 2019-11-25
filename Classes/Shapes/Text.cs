using csharp_08.Utils;
using System;
using System.Collections.Generic;

namespace csharp_08
{
    /// <summary>
    /// Class that describes a Text object from the writeboard.
    /// This class is heriting from the class Shape.
    /// </summary>
    public class Text : Shape
    {
        public string InnerText { get; private set; }
        public uint FontSize { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Vertices">The coordinates of the points that describe the location of the Text object</param>
        /// <param name="Owner">The user owning the Text object</param>
        /// <param name="InnerText">Text written in the Text object</param>
        /// <param name="FontSize">Font size of the text</param>
        /// <param name="Config">The graphical configuration of the text</param>
        /// <param name="ID">The unique id of the shape</param>
        public Text(List<Tuple<double, double>> Vertices, User Owner, string InnerText, uint FontSize, ShapeConfig Config, uint ID) :
               base(Vertices, Owner, Config, ID)
        {
            this.InnerText = InnerText;
            this.FontSize = FontSize;
            this.Code = ShapeCode.Text;
        }

        public override string ToString()
        {
            return "Forme: Text\nText: " + InnerText + "\n" + base.ToString();
        }

        public override byte GetShapeCode()
        {
            return (byte)ShapeCode.Text;
        }

        public override string ToSVG()
        {
            return String.Format("<text x=\"{0}\" y=\"{1}\" font-family=\"arial\" font-size=\"{2}\" fill=\"{3}\">{4}</text>",
                                 Vertices[0].Item1,
                                 Vertices[0].Item2,
                                 FontSize,
                                 Config.Color.IsEmpty ? "#00000000" : ShapeUtils.ColorToHex(Config.Color),
                                 InnerText);
        }
    }
}