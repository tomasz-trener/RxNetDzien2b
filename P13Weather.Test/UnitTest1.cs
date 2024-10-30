using Moq;
using P10DequenceEmission;
using System.Reactive.Linq;

namespace P13Weather.Test
{
    public class UnitTest1
    {
        [Fact]
        public void GetWeatherForeast_REturnsCorrentData_WhenApiREturnsValidData()
        {
            var mockHttp = new Mock<HttpClient>(); 
            var weatherService = new Mock<WeatherService>(mockHttp.Object);

            string testWeatherData = "Date : 2021-09-01, TemperatureC: 42, Summary: Hot";

            weatherService.Setup(x => x.GetWeatherForecast(It.IsAny<string>())).Returns(Observable.Return(testWeatherData));


            weatherService.Object.GetWeatherForecast("http://localhost:5043/weatherforecast").Subscribe(
                x => Assert.Equal(testWeatherData, x),
                error => Assert.True(false, error.Message));
        }

        [Fact]
        public void GetWeatherForeast_Error_WhenApiFails()
        {
            var mockHttp = new Mock<HttpClient>();
            var weatherService = new Mock<WeatherService>(mockHttp.Object);

            string testWeatherData = "0";

            weatherService.Setup(x => x.GetWeatherForecast(It.IsAny<string>()))
                .Returns(Observable.Throw<string>(new Exception("Api error"))
                .Catch(Observable.Return("0")));


            weatherService.Object.GetWeatherForecast("http://localhost:5043/weatherforecast").Subscribe(
                x => Assert.Equal(testWeatherData, x),
                error => Assert.True(false, error.Message));
        }
    }
}