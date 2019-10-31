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
        /// <summary>
        ///     Determines whether this instance can convert the specified object type.
        ///     So return true if the given objectType is type of Color.
        /// </summary>
        /// <param name="objectType"></param>
        /// <returns>Boolean</returns>
        public override bool CanConvert(Type objectType)
        {
            return typeof(Color) == objectType;
        }

        /// <summary>
        ///     Reads the JSON representation of the object.
        ///     Get object from the reader that should be a string of hex value and return the corresponding Color instance.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="objectType"></param>
        /// <param name="existingValue"></param>
        /// <param name="serializer"></param>
        /// <returns></returns>
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            // Load token from stream
            JToken token = JToken.Load(reader);
            // Return a Color instance
            return Convert(token.Value<string>());
        }

        /// <summary>
        ///     Not used.
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="serializer"></param>
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Convert a hex string to Color Instance.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private Color Convert(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return Color.Empty;
            }
            if (value.Length != 7)
            {
                throw new FormatException($"Unable to convert '{value}' to a Color. Requires string length of 7 including the leading hashtag.");
            }

            int red = HexToInt(value.Substring(1, 2));
            int green = HexToInt(value.Substring(3, 2));
            int blue = HexToInt(value.Substring(5, 2));
            return Color.FromArgb(red, green, blue);
        }

        /// <summary>
        ///     Convert a string hex value to number.
        /// </summary>
        /// <param name="hexValue"></param>
        /// <returns></returns>
        private int HexToInt(string hexValue)
        {
            if (hexValue.Length != 2)
            {
                throw new FormatException($"Unable to convert '{hexValue}' into a number between 0 and 255. Requires string length of 2.");
            }

            if (!int.TryParse(hexValue, NumberStyles.HexNumber, null, out int result))
            {
                throw new FormatException($"Invalid hex number: '{hexValue}'");
            }

            return result;
        }
    }
}
