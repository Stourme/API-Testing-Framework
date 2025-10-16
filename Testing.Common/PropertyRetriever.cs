using Newtonsoft.Json.Linq;

namespace Testing.Common
{
    /// <summary>
    /// PropertyRetriever reads and returns values from the configuration files
    /// </summary>
    public static class PropertyRetriever
    {
        /// <summary>
        /// Searches all defined configuration repos for the requested property
        /// </summary>
        /// <param name="keyValue">Key to search for</param>
        /// <returns>property value</returns>
        public static string? GetProperty(string keyValue)
        {
            string? environment = GetEnvironment();
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Configuration");
            string propertiesFile = string.Empty;

            if (environment != null)
            {
                propertiesFile = environment + "_properties.json";
            }

            string completePropertiesFilePath = Path.Combine(filePath, propertiesFile);

            try
            {
                using (StreamReader reader = new StreamReader(completePropertiesFilePath))
                {
                    string jsonFile = reader.ReadToEnd();
                    JObject jsonObject = JObject.Parse(jsonFile);

                    if (jsonObject[keyValue] != null)
                    {
                        return jsonObject[keyValue].ToString();
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Retrieves the environment value from the launchsettings file
        /// </summary>
        /// <returns>string</returns>
        public static string? GetEnvironment()
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "launchsettings.json");

            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string jsonFile = reader.ReadToEnd();
                    var jsonObject = JObject.Parse(jsonFile);
                    if (jsonObject["environment"] != null)
                    {
                        return jsonObject["environment"]?.ToString();
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
