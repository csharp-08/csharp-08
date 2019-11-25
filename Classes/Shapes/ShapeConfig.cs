using System.Drawing;

namespace csharp_08
{
    /// <summary>
    /// An object that store the graphical configuration of a shape.
    /// It stores the position, the shape, the color, and many other parameters.
    /// </summary>
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

        /// <summary>
        /// Constructor of the ShapeConfig object.
        /// </summary>
        /// <param name="Thickness">The thickness of the shape's lines</param>
        /// <param name="Color">The color of the shape</param>
        /// <param name="BorderColor">The color of the border of the shape</param>
        /// <param name="OffsetX">The position on the x-axis of the shape on the canvas</param>
        /// <param name="OffsetY">The position on the y-axis of the shape on the canvas</param>
        /// <param name="ScaleX">The scale of x-axis (scale = 1.0 is normal, scale = 2.0 stretches the shape of a factor 2)</param>
        /// <param name="ScaleY">The scale of y-axis (scale = 1.0 is normal, scale = 2.0 stretches the shape of a factor 2)</param>
        /// <param name="Rotate">The rotation degree of the shape</param>
        /// <param name="IsEmpty">Tells if the object is empty inside or not</param>
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

        /// <summary>
        /// Default configuration of style for a shape.
        /// </summary>
        public static ShapeConfig DefaultConfig { get; } = new ShapeConfig(10, new Color(), Color.Black, 0, 0, 1, 1, 0, false);

        /// <summary>
        /// ToString function which describes the object with a string
        /// </summary>
        /// <returns>String describing the object</returns>
        public override string ToString()
        {
            string str = $"Congig(Thickness: {Thickness}, Color: {Color}, BorderColor: {BorderColor}, OffsetX: {OffsetX}, ";
            str += $"OffsetY: {OffsetY}, ScaleX: {ScaleX}, ScaleY: {ScaleY}, Rotation: {Rotation}, IsEmpty: {IsEmpty} )";
            return str;
        }
    }
}