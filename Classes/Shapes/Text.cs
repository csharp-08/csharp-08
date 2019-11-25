using csharp_08.Utils;
using System;
using System.Collections.Generic;

namespace csharp_08
{
    public class Text : Shape
    {
        public string InnerText { get; private set; }
        public uint FontSize { get; private set; }

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