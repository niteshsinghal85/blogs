using Microsoft.AspNetCore.Mvc;

namespace awesomemovie.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class MovieController : ControllerBase
{
    private readonly ILogger<MovieController> _logger;
    
    public MovieController(ILogger<MovieController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public Movie Get()
    {
        _logger.LogInformation("Getting movie list in controller.");
        var movie = new Movie
        {
            Id = 1,
            Title = "Inception",
            ReleaseDate = new DateOnly(2010, 07, 1),
            Language = "English",
            Rating = 9.8f
        };
        return movie;
    }
}
