
namespace Testing.Common
{
    /// <summary>
    /// Builds a route based on the RouteType enum
    /// </summary>
    public class RouteFactory
    {
        /// <summary>
        /// Full route
        /// </summary>
        public string Route { get; set; }
        /// <summary>
        /// private constructor
        /// </summary>
        /// <param name="version">Api Version</param>
        /// <param name="routeType">Route type</param>
        /// <param name="id">object id</param>
        private RouteFactory(ApiVersion version, Enum routeType, string?
       data = null)
        {
            Route = $"/{version}/{routeType.GetDescription(data)}";
        }
        /// <summary>
        /// private constructor
        /// </summary>
        /// <param name="version">Api Version</param>
        /// <param name="routeType">Route type</param>
        /// <param name="valuePairs">Dictionary</param>
        private RouteFactory(ApiVersion version, Enum routeType, Dictionary<string, string> valuePairs)
        {
            Route = $"/{version}/{routeType.GetDescription(valuePairs)}";
        }
        /// <summary>
        /// Passes variables to the constructor
        /// </summary>
        /// <param name="version">Api version</param>
        /// <param name="routeType">route type</param>
        /// <param name="id">branch or operation id</param>
        /// <returns>OrganizationsrouteFactory</returns>
        public static RouteFactory Build<T>(ApiVersion version, T routeType, string id = null) where T :
                    Enum => new RouteFactory(version, routeType, id);

        public static RouteFactory Build<T>(ApiVersion version, T routeType, Dictionary<string, string> valuePairs) where T :
                    Enum => new RouteFactory(version, routeType, valuePairs);
    }


}
