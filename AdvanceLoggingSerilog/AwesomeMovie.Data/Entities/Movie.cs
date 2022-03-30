using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwesomeMovie.Data.Entities
{
	public class Movie
	{
		public int Id { get; set; }

		public string? Title { get; set; }

		public DateTime ReleaseDate { get; set; }

		public float Rating { get; set; }

		public string? Language { get; set; }
	}
}
