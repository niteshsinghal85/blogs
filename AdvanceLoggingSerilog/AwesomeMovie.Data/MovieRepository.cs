using AwesomeMovie.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AwesomeMovie.Data
{
	public class MovieRepository : IMovieRepository
	{
		private readonly AppDbContext  _context;
		private readonly ILogger<MovieRepository> _logger;

		public MovieRepository(AppDbContext context, ILogger<MovieRepository> logger)
		{
			_context = context;
			_logger = logger;
		}

		public async Task<Movie?> GetMovieByIdAsync(int id)
		{
			_logger.LogInformation("Getting a Movie for ID: {id} in repository", id);
			if (id == 0)
			{
				var ex = new ArgumentException("Invalid Id");
				ex.Data.Add("Id", id);
				throw ex;
			}
			return await _context.Movies.FindAsync(id);
		}

		public async Task<List<Movie>> GetMoviesAsync()
		{
			_logger.LogInformation("Getting Movie list in repository");
			return await _context.Movies.ToListAsync();
        }
	}
}
