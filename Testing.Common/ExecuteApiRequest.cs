using RestSharp;

namespace Testing.Common
{
    public static class ExecuteApiRequest
    {
        /// <summary>
        /// HTTP Post method
        /// </summary>
        /// <param name="path">API route</param>
        /// <param name="json">json package to send</param>
        /// <param name="bearerToken">The bearer token to authenticate the request.</param>
        public static RestResponse<T> Post<T>(string path, object json, string? bearerToken) where T : class
        {
            string? baseUrl = PropertyRetriever.GetProperty("baseUrl");

            if (string.IsNullOrEmpty(baseUrl))
            {
                throw new InvalidOperationException("Base URL is not configured.");
            }

            using RestClient client = RestClientFactory.CreateClient(baseUrl);
            RestRequest request = new(path, Method.Post);
            request.AddHeader("Content-Type", "application/json");
            if (bearerToken is not null)
            {
                request.AddHeader("Authorization", "Bearer " +
               bearerToken);
            }
            request.AddJsonBody(json);

            //Opening this line up so that breakpoints can be added to this line to check the http response as it happens.
            //Needed to make debugging easier
            var response = client.Execute<T>(request);

            return response;
        }

        /// <summary>
        /// HTTP Get method
        /// </summary>
        /// <param name="path">API route</param>
        /// <param name="bearerToken">The bearer token to authenticate the request.</param>
        public static RestResponse<T>? Get<T>(string path, string? bearerToken) where T : class
        {
            RestResponse<T>? response = null;

            string? baseUrl = PropertyRetriever.GetProperty("baseUrl");

            if (string.IsNullOrEmpty(baseUrl))
            {
                throw new InvalidOperationException("Base URL is not configured.");
            }

            using RestClient client = RestClientFactory.CreateClient(baseUrl);
            RestRequest request = new(path, Method.Get);
            request.AddHeader("Content-Type", "application/json");

            if (bearerToken is not null)
            {
                request.AddHeader("Authorization", "Bearer " +
               bearerToken);
            }

            Retry.UntilTrueOrTimeOut(() => GetResponse<T>(client, request, out response));
            return response;
        }

        /// <summary>
        /// HTTP Get method
        /// </summary>
        /// <param name="path">API route</param>
        /// <param name="json">json load</param>
        /// <param name="bearerToken">The bearer token to authenticate the request.</param>
        public static RestResponse<T>? Get<T>(string path, object json, string? bearerToken) where T : class
        {
            RestResponse<T>? response = null;
            string? baseUrl = PropertyRetriever.GetProperty("baseUrl");

            if (string.IsNullOrEmpty(baseUrl))
            {
                throw new InvalidOperationException("Base URL is not configured.");
            }

            using RestClient client = RestClientFactory.CreateClient(baseUrl);
            RestRequest request = new(path, Method.Get);
            request.AddHeader("Content-Type", "application/json");

            if (bearerToken is not null)
            {
                request.AddHeader("Authorization", "Bearer " +
               bearerToken);
            }

            request.AddJsonBody(json);
            Retry.UntilTrueOrTimeOut(() => GetResponse(client, request, out response));
            return response;
        }

        /// <summary>
        /// Http Put method
        /// </summary>
        /// <param name="path">Http path</param>
        /// <param name="json">json data</param>
        /// <param name="bearerToken">The bearer token to authenticate the request.</param>
        public static RestResponse<T> Put<T>(string path, object json, string? bearerToken) where T : class
        {
            string? baseUrl = PropertyRetriever.GetProperty("baseUrl");

            if (string.IsNullOrEmpty(baseUrl))
            {
                throw new InvalidOperationException("Base URL is not configured.");
            }

            using RestClient client = RestClientFactory.CreateClient(baseUrl);
            RestRequest request = new(path, Method.Put);
            request.AddHeader("Content-Type", "application/json");

            if (bearerToken is not null)
            {
                request.AddHeader("Authorization", "Bearer " +
               bearerToken);
            }

            request.AddJsonBody(json);
            var putResponse = client.Execute<T>(request);
            return putResponse;
        }

        /// <summary>
        /// Http delete method
        /// </summary>
        /// <param name="path">Http path</param>
        /// <param name="bearerToken">The bearer token to authenticate the request.</param>
        public static RestResponse<T> Delete<T>(string path, string? bearerToken) where T : class
        {
            string? baseUrl = PropertyRetriever.GetProperty("baseUrl");

            if (string.IsNullOrEmpty(baseUrl))
            {
                throw new InvalidOperationException("Base URL is not configured.");
            }

            using RestClient client = RestClientFactory.CreateClient(baseUrl);
            RestRequest request = new(path, Method.Delete);
            request.AddHeader("Content-Type", "application/json");

            if (bearerToken is not null)
            {
                request.AddHeader("Authorization", "Bearer " +
               bearerToken);
            }

            return client.Execute<T>(request);
        }
        /// <summary>
        /// Http delete method
        /// </summary>
        /// <param name="path">Http path</param>
        /// <param name="bearerToken">The bearer token to authenticate the request.</param>
        public static RestResponse Delete(string path, string? bearerToken)
        {
            string? baseUrl = PropertyRetriever.GetProperty("baseUrl");

            if (string.IsNullOrEmpty(baseUrl))
            {
                throw new InvalidOperationException("Base URL is not configured.");
            }

            using RestClient client = RestClientFactory.CreateClient(baseUrl);
            RestRequest request = new(path, Method.Delete);
            request.AddHeader("Content-Type", "application/json");

            if (bearerToken is not null)
            {
                request.AddHeader("Authorization", "Bearer " + bearerToken);
            }

            return client.Delete(request);
        }

        /// <summary>
        /// Execute Get method to use with Retry
        /// </summary>
        /// <typeparam name="T">DTO type</typeparam>
        /// <param name="client">Rest Client</param>
        /// <param name="request">Rest Request</param>
        /// <param name="response">RestResponse</param>
        private static bool GetResponse<T>(RestClient client, RestRequest request, out RestResponse<T> response) where T : class
        {
            response = client.Execute<T>(request);
            return response?.Data != null;
        }
        /// <summary>
        /// Execute Get method to use with Retry
        /// </summary>
        /// <typeparam name="T">DTO type</typeparam>
        /// <param name="client">Rest Client</param>
        /// <param name="request">Rest Request</param>
        /// <param name="response">RestResponse</param>
        private static bool GetResponse(RestClient client, RestRequest
       request, out RestResponse response)
        {
            response = client.Execute(request);
            return response != null;
        }

    }
}
