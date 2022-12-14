using System.Collections.Generic;

namespace WeatherMicroservice;

public record ForecastResponseModel(List<WeatherForecast> Forecasts);
