using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SQLite;
using System.Collections.Generic;

namespace csharp_08
{
    public class Canvas
    {
        [Ignore]
        public Dictionary<uint, Shape> Shapes { get; private set; }

        private static uint IDs = 0;

        [PrimaryKey, Unique]
        public uint Id { get; set; }

        [MaxLength(100000)]
        public string SerializedShapes { get; set; }

        public Canvas()
        {
            this.Shapes = new Dictionary<uint, Shape>();
            Id = IDs;
            IDs++;
        }

        public static void SetIdStartPoint(uint StartPoint)
        {
            IDs = StartPoint;
        }

        public void Serialize()
        {
            SerializedShapes = JsonConvert.SerializeObject(Shapes);
        }

        public void Deserialize()
        {
            Shapes = new Dictionary<uint, Shape>();

            Dictionary<uint, JObject> shapes_tmp = JsonConvert.DeserializeObject<Dictionary<uint, JObject>>(SerializedShapes);
            uint LastShapeId = 0;
            foreach (var shape in shapes_tmp)
            {
                Shape tmp = shape.Value.ToObject<Pencil>();
                switch (tmp.Code)
                {
                    case ShapeCode.Line:
                        Shapes.Add(shape.Key, shape.Value.ToObject<Line>());
                        break;

                    case ShapeCode.Pencil:
                        Shapes.Add(shape.Key, shape.Value.ToObject<Pencil>());
                        break;

                    case ShapeCode.Circle:
                        Shapes.Add(shape.Key, shape.Value.ToObject<Circle>());
                        break;

                    case ShapeCode.Text:
                        Shapes.Add(shape.Key, shape.Value.ToObject<Text>());
                        break;

                    case ShapeCode.Polygon:
                        Shapes.Add(shape.Key, shape.Value.ToObject<Polygon>());
                        break;

                    case ShapeCode.Point:
                        Shapes.Add(shape.Key, shape.Value.ToObject<Point>());
                        break;

                    default:
                        break;
                }
                LastShapeId = shape.Key > LastShapeId ? shape.Key : LastShapeId;
            }

            Shape.UpdateStartPoint(LastShapeId + 1);
        }
    }
}