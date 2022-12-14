using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace WeatherMicroservice.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] _summaries =
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    [HttpGet]
    public IEnumerable<WeatherForecast> Get()
    {
        var random = new Random();
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = random.Next(-20, 55),
                Summary = _summaries[random.Next(_summaries.Length)]
            })
            .ToArray();
    }

    [HttpGet("/{personName}")]
    public string GetForPerson([FromRoute] string personName)
    {
        var random = new Random();
        return $"Weather for {personName}: {_summaries[random.Next(_summaries.Length)]}";
    }
}
