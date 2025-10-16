using FluentAssertions;
using RestSharp;

namespace Testing.Common
{
    public static class ResponseValidator
    {
        public static void ValidateResponseAndData<T>(RestResponse<T>? response) where T : class
        {
            response.Should().NotBeNull("Response should not be null");
            response.IsSuccessful.Should().BeTrue($"Response was not successful. Status Code: {response.StatusCode}, Content: {response.Content}");
            response.Data.Should().NotBeNull("Response data should not be null");

        }
    }
}
