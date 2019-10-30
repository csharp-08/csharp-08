using System;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace csharp_08
{
    public class ColorJsonConverter : JsonConverter
    {

        public override bool CanConvert(Type objectType)
        {
            return typeof(Color) == objectType;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            // Load token from stream
            var token = JToken.Load(reader);
            Debug.WriteLine("token :");
            Debug.WriteLine(token);

            var target = Convert(token.Value<string>());

            Debug.WriteLine("return :");
            Debug.WriteLine(target);

            return target;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var valueToSerialize = Convert((Color) value);
            serializer.Serialize(writer, valueToSerialize);
        }

        private string Convert(Color value)
        {
            return value.IsEmpty ? string.Empty : $"#{value.R.ToString("X2")}{value.G.ToString("X2")}{value.B.ToString("X2")}";
        }

        private Color Convert(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return Color.Empty;
            }
            if (value.Length != 7)
            {
                var message = $"Unable to convert '{value}' to a Color. Requires string length of 7 including the leading hashtag.";
                throw new FormatException(message);
            }

            var red = HexToInt(value.Substring(1, 2));
            var green = HexToInt(value.Substring(3, 2));
            var blue = HexToInt(value.Substring(5, 2));
            return Color.FromArgb(red, green, blue);
        }

        private int HexToInt(string hexValue)
        {
            if (hexValue.Length != 2)
            {
                var message = $"Unable to convert '{hexValue}' to an Integer. Requires string length of 2.";
                throw new FormatException(message);
            }

            if (!int.TryParse(hexValue, NumberStyles.HexNumber, null, out int result))
            {
                throw new FormatException($"Invalid hex number: {hexValue}");
            }

            return result;
        }
    }
}
