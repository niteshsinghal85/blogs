using AwesomeMovie.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeMovie.Data
{
	public interface IMovieRepository
	{
		Task<List<Movie>> GetMoviesAsync();
		Task<Movie?> GetMovieByIdAsync(int id);
	}
}
