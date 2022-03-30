using AwesomeMovie.Data;
using AwesomeMovie.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace awesomemovie.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class MovieController : ControllerBase
{
    private readonly ILogger<MovieController> _logger;
    private readonly IMovieRepository _repository;

    public MovieController(ILogger<MovieController> logger, IMovieRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    [HttpGet]
    public async Task<IEnumerable<Movie>> Get()
    {
        _logger.LogInformation("Getting movie list in controller.");
        return await _repository.GetMoviesAsync();
        
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(Movie), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int id)
    {
        _logger.LogDebug("Getting single movie in controller for ID: {id}", id);
        var movie = await _repository.GetMovieByIdAsync(id);
        if (movie != null)
        {
            return Ok(movie);
        }
        _logger.LogWarning("No movie found for ID: {id}", id);
        return NotFound();
    }
}
