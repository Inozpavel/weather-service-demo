using NUnit.Framework;
using WeatherMicroservice.Controllers;

namespace WeatherMicroservice.Tests;

[TestFixture]
public class WeatherForecastControllerTests
{
    [Test]
    [TestCase("Name")]
    [TestCase("Name with space")]
    [TestCase("123")]
    public void CanFetForecastForUser(string username)
    {
        var controller = new WeatherForecastController();

        var result = controller.GetForPerson(username);

        Assert.True(result.Forecast.Contains(username));
    }
}
