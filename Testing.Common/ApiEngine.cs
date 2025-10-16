
using RestSharp;

namespace Testing.Common
{
    /// <summary>
    /// ApiEngine for generic functionality
    /// </summary>
    public class ApiEngine
    {
        private readonly string _bearerToken;
        public ApiEngine(string bearerToken)
        {
            _bearerToken = bearerToken;
        }

        /// <summary>
        /// Sends a Get request to the api
        /// </summary>
        /// <typeparam name="T">Response type</typeparam>
        /// <param name="routeFactory">Route</param>
        /// <param name="validate">bool to use validation, no validation required here for negative tests</param>
        /// <param name="bearerToken">The bearer token to authenticate the request.</param>
        public RestResponse<T> Get<T>(RouteFactory routeFactory, bool validate = true) where T : class
        {
            RestResponse<T>? response = ExecuteApiRequest.Get<T>(routeFactory.Route, _bearerToken);
            
            if (validate)
            {
                ResponseValidator.ValidateResponseAndData(response);
            }

            return response;
        }

        /// <summary>
        /// Sends a Get request to the api
        /// </summary>
        /// <typeparam name="T">Response type</typeparam>
        /// <param name="routeFactory">Route</param>
        /// <param name="json">json load</param>
        /// <param name="validate">bool to use validation, no validation required here for negative tests</param>
        /// <param name="bearerToken">The bearer token to authenticate the request.</param>
        public RestResponse<T>? Get<T>(RouteFactory routeFactory, object json, bool validate = true) where T : class
        {
            RestResponse<T>? response = ExecuteApiRequest.Get<T>(routeFactory.Route, json, _bearerToken);

            if (validate)
            {
                ResponseValidator.ValidateResponseAndData(response);
            }

            return response;
        }

        /// <summary>
        /// Sends a Put request to the api
        /// </summary>
        /// <typeparam name="T">Response type</typeparam>
        /// <param name="routeFactory">api route</param>
        /// <param name="json">json load</param>
        /// <param name="bearerToken">The bearer token to authenticate the request.</param>
        public RestResponse<T> Put<T>(RouteFactory routeFactory, object json, bool validate = true) where T : class
        {
            RestResponse<T> response = ExecuteApiRequest.Put<T>(routeFactory.Route, json, _bearerToken);

            if (validate)
            {
                ResponseValidator.ValidateResponseAndData(response);
            }

            return response;
        }
        /// <summary>
        /// Sends a Delete request to the api
        /// </summary>
        /// <param name="routeFactory">api route</param>
        /// <param name="bearerToken">The bearer token to authenticate the request.</param>
        public void Delete(RouteFactory routeFactory)
        {
            ExecuteApiRequest.Delete(routeFactory.Route, _bearerToken);
        }

        /// <summary>
        /// Sends a Delete request to the api
        /// </summary>
        /// <param name="routeFactory">api route</param>
        /// <param name="bearerToken">The bearer token to authenticate the request.</param>
        public RestResponse<T> Delete<T>(RouteFactory routeFactory) where T : class
        {
            RestResponse<T> response = ExecuteApiRequest.Delete<T>
            (routeFactory.Route, _bearerToken);
            return response;
        }
        /// <summary>
        /// Sends a Post to the api
        /// </summary>
        /// <typeparam name="T">Response type</typeparam>
        /// <param name="routeFactory">endpoint route</param>
        /// <param name="json">json load</param>
        /// <param name="bearerToken">The bearer token to authenticate the request.</param>
        public RestResponse<T> Post<T>(RouteFactory routeFactory, object json, bool validate = true) where T : class
        {
            RestResponse<T> response = ExecuteApiRequest.Post<T>
            (routeFactory.Route, json, _bearerToken);
            if (validate)
            {
                ResponseValidator.ValidateResponseAndData(response);
            }
            return response;
        }


    }
}
