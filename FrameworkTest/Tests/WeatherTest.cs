using FluentAssertions;
using FrameworkTest.Contracts;

using Testing.Common;

namespace FrameworkTest.Tests
{
    public class WeatherTest : TestBase
    {
        //Quick test to verify the framework is working as expected.
        [Fact]
        public void FrameworkTest_GetWeather()
        {
            //This one line is all that's needed for a test to GET a return from an endpoint in this framework.
            //The response is automatically checked for null values in the Get method.
            var response = ApiEngine.Get<List<WeatherForecast>>(RouteFactory.Build(ApiVersion.V1, WeatherRoute.WeatherForecast));

            //Validate the return.
            //Possible list of returns.
            List<string> weatherList = new() 
            { 
                "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" 
            };

            List<string> actualList = new();
            //Actual list of returns.
            //The framework automatically checks for null values in the response.
            foreach (var weather in response.Data)
            {
                actualList.Add(weather.Summary);
            }

            bool containsAny = actualList.Intersect(weatherList).Any();

            containsAny.Should().BeTrue();

        }



    }
}
