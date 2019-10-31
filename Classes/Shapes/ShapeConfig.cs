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

        public ShapeConfig(int Thickness = 10, Color Color = new Color(), Color BorderColor = new Color(),
                           double OffsetX = 0, double OffsetY = 0, double ScaleX = 1,
                           double ScaleY = 1, double Rotate = 0, bool IsEmpty = false)
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

        public static ShapeConfig DefaultConfig { get; } = new ShapeConfig(10, new Color(), Color.Black, 0, 0, 1, 1, 0, false);

        public override string ToString()
        {
            string str = $"Congig(Thickness: {Thickness}, Color: {Color}, BorderColor: {BorderColor}, OffsetX: {OffsetX}, ";
            str += $"OffsetY: {OffsetY}, ScaleX: {ScaleX}, ScaleY: {ScaleY}, Rotation: {Rotation}, IsEmpty: {IsEmpty} )";
            return str;
        }
    }
}