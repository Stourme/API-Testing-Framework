using RestSharp;

using System.Collections.Concurrent;

namespace Testing.Common
{
    public static class RestClientFactory
    {
        // Cache RestClient instances per normalized base URL to improve reuse and reduce socket exhaustion.
        private static readonly ConcurrentDictionary<string, RestClient> _clients = new();

        /// <summary>
        /// Create an instance of <see cref="RestClient"/>.
        /// </summary>
        /// <param name="baseUrl">Domain URL (absolute).</param>
        /// <param name="reuse">When true, reuse a cached client for the same base URL.</param>
        /// <param name="timeout">Optional request timeout. If null, RestSharp defaults are used.</param>
        /// <param name="defaultHeaders">Optional additional default headers to add.</param>
        /// <returns>RestSharp <see cref="RestClient"/></returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="baseUrl"/> is null, empty, or not an absolute URI.</exception>
        public static RestClient CreateClient(
            string baseUrl,
            bool reuse = true,
            TimeSpan? timeout = null,
            IReadOnlyDictionary<string, string>? defaultHeaders = null)
        {
            if (string.IsNullOrWhiteSpace(baseUrl))
                throw new ArgumentException("Base URL cannot be null or whitespace.", nameof(baseUrl));

            if (!Uri.TryCreate(baseUrl, UriKind.Absolute, out var baseUri))
                throw new ArgumentException("Base URL must be an absolute URI.", nameof(baseUrl));

            // Normalize key to avoid duplicates because of trailing slashes.
            var key = baseUri.ToString().TrimEnd('/');

            RestClient CreateNewClient()
            {
                var client = new RestClient(baseUri);

                // Optional timeout (RestSharp exposes options where available)
                if (timeout.HasValue)
                {
                    try
                    {
                        // If RestClient.Options is available and has a Timeout property, set it here.
                        // This is the supported way in RestSharp >=107.0.0.
                        var optionsProperty = client.GetType().GetProperty("Options");
                        if (optionsProperty != null)
                        {
                            var options = optionsProperty.GetValue(client);
                            var timeoutProp = options?.GetType().GetProperty("Timeout");
                            if (timeoutProp != null && timeoutProp.CanWrite)
                            {
                                timeoutProp.SetValue(options, timeout.Value);
                            }
                        }
                    }
                    catch
                    {
                        //API mismatch.
                    }
                }

                // Always accept JSON by default
                client.AddDefaultHeader("Accept", "application/json");

                if (defaultHeaders != null)
                {
                    foreach (var kv in defaultHeaders)
                        client.AddDefaultHeader(kv.Key, kv.Value);
                }

                return client;
            }

            return reuse
                ? _clients.GetOrAdd(key, _ => CreateNewClient())
                : CreateNewClient();
        }

        /// <summary>
        /// Convenience overload that mirrors the previous behaviour: creates or reuses a client with "Accept: application/json".
        /// </summary>
        public static RestClient CreateClient(string baseUrl) => CreateClient(baseUrl, reuse: true, timeout: null, defaultHeaders: null);
    }
}
