using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;

namespace csharp_08
{
    public class ShapeConfig
    {
        public int Thickness { get; private set; }
        public Color Color { get; private set; }
        public Color BorderColor { get; private set; }
        public double OffsetX { get; private set; }
        public double OffsetY { get; private set; }
        public double ScaleX { get; private set; }
        public double ScaleY { get; private set; }
        public double Rotation { get; private set; }
        public bool IsEmpty { get; private set; }

        public ShapeConfig(int Thickness, Color Color, Color BorderColor, 
                           double OffsetX, double OffsetY, double ScaleX,
                           double ScaleY, double Rotate, bool IsEmpty)
        {
            this.Thickness = Thickness;
            this.Color = Color;
            this.BorderColor = BorderColor;
            this.OffsetX = OffsetX;
            this.OffsetY = OffsetY;
            this.ScaleX = ScaleX;
            this.ScaleY = ScaleY;
            this.Rotation = Rotate;
            this.IsEmpty = IsEmpty;
        }
    }
}
