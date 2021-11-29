using Swashbuckle.AspNetCore.Annotations;

namespace DemoSwaggerAnnotation;

public class WeatherForecast
{
    [SwaggerSchema(Description = "Date and time for weather")]
    [SwaggerSchemaExample("2021-10-30T10:42:53.531Z")]
    public DateTime Date { get; set; }

    [SwaggerSchema(Description = "Temperature in celcius")]
    [SwaggerSchemaExample("40")]
    public int TemperatureC { get; set; }

    [SwaggerSchema(Description = "Temperature in fahrenheit")]
    [SwaggerSchemaExample("103")]
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
    
    [SwaggerSchema(Description = "Summary weather forecast ")]
    [SwaggerSchemaExample("Sweltering")]
    public string? Summary { get; set; }
}
