using Microsoft.AspNetCore.Mvc;

namespace DemoFileUpload.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;

    public WeatherForecastController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

	//[HttpGet(Name = "GetWeatherForecast")]
	//public IEnumerable<WeatherForecast> Get()
	//{
	//	return Enumerable.Range(1, 5).Select(index => new WeatherForecast
	//	{
	//		Date = DateTime.Now.AddDays(index),
	//		TemperatureC = Random.Shared.Next(-20, 55),
	//		Summary = Summaries[Random.Shared.Next(Summaries.Length)]
	//	})
	//	.ToArray();
	//}

	[HttpPost("UploadImage")]
    public ActionResult UploadImage()
    {
        foreach (var item in HttpContext.Request.Form.Files)
	    {
            _logger.LogInformation("file uploaded : " + item.FileName);
        }
	    // we can rest of upload logic here.
	    return Ok();
    }

    [HttpPost("AddData")]
    public ActionResult AddData([FromBody] string data)
    {
        // we can rest of upload logic here.
        return Ok();
    }

    [HttpPost("UploadMultipeImages")]
    public ActionResult UploadMultipeImages(IFormFile[] files)
    {
        foreach (var item in files)
        {
            _logger.LogInformation("file uploaded : " + item.FileName);
        }
        // we can rest of upload logic here.
        return Ok();
    }

}
