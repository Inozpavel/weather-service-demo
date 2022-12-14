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
    public ForecastResponseModel Get()
    {
        var random = new Random();
        return new ForecastResponseModel(Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = random.Next(-20, 55),
                Summary = _summaries[random.Next(_summaries.Length)]
            })
            .ToList());
    }

    [HttpGet("{personName}")]
    public ForecastForPerson GetForPerson([FromRoute] string personName)
    {
        var random = new Random();
        return new ForecastForPerson($"Weather for {personName}: {_summaries[random.Next(_summaries.Length)]}");
    }
}
