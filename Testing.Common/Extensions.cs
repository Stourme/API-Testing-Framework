using System.ComponentModel;
using System.Reflection;

namespace Testing.Common
{
    /// <summary>
    /// Shared extensions
    /// </summary>
    public static class Extensions
    {
        private static readonly Random random = new();
        /// <summary>
        /// Returns the description after swapping {data} with a value
        /// </summary>
        /// <param name="value">the enum option selected</param>
        /// <param name="data">value to be inserted into the route</param>
        /// <returns>modified enum descritption as a string</returns>
        public static string? GetDescription(this Enum value, string data)
        {
            var description = value.GetType()
                       .GetField(value.ToString()!)?
                       .GetCustomAttribute<DescriptionAttribute>()?
                       .Description
                        ?? value.ToString();

            return description.Replace("{data}", data);
        }

        /// <summary>
        /// Returns the description after swapping custom identifiers from a key/value pair
        /// </summary>
        /// <param name="value">Enum</param>
        /// <param name="valuePairs">Dictionary</param>
        /// <returns>string</returns>
        public static string GetDescription(this Enum value, Dictionary<string, string> valuePairs)
        {
            DescriptionAttribute? attribute = value.GetType()
                                       .GetField(value.ToString()!)?
                                       .GetCustomAttribute<DescriptionAttribute>();

            string description = attribute == null ? value.ToString() : attribute.Description;

            foreach (KeyValuePair<string, string> key in valuePairs)
            {
                description = description.Replace(key.Key, key.Value);
            }

            return description;
        }

        /// <summary>
        /// Combines input to create a unique string to be used in test data
        /// </summary>
        /// <param name="input">string</param>
        /// <returns>string</returns>
        public static string CreateUnique(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                input = "NullOrEmpty";
            }
            return $"{input}{random.Next(1, 10000)}";
        }
    }
}

