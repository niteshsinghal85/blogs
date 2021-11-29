namespace DemoSwaggerAnnotationWithXmlComments;

public class WeatherForecast
{

    /// <summary>
    /// "Date and time for weather"
    /// </summary>
    /// <example>2021-10-30T10:42:53.531Z</example>
    public DateTime Date { get; set; }

    /// <summary>
    /// Temperature in celcius
    /// </summary>
    /// <example>40</example>
    public int TemperatureC { get; set; }

    /// <summary>
    /// Temperature in fahrenheit
    /// </summary>
    /// <example>103</example>
    
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    /// <summary>
    /// Summary weather forecast 
    /// </summary>
    /// <example>Sweltering</example>
    public string? Summary { get; set; }
}
