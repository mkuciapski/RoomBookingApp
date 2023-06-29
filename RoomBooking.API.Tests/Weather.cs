using Microsoft.Extensions.Logging;
using Moq;
using RoomBooking.API.Controllers;
using Shouldly;

namespace RoomBooking.API.Tests
{
    public class Weather
    {
        [Fact]
        public void ShouldReturnWeatherForcast()
        {
            var loggerMock = new Mock<ILogger<WeatherForecastController>>();
            var controller = new WeatherForecastController(loggerMock.Object);

            var result = controller.Get();

            result.Count().ShouldBeGreaterThan(1);
            result.ShouldNotBeNull();
        }
    }
}