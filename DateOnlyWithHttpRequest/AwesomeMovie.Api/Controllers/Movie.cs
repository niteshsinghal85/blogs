namespace awesomemovie.Api.Controllers
{
	public class Movie
	{
		public int Id { get; set; }

		public string? Title { get; set; }

		public DateOnly ReleaseDate { get; set; }

		public float Rating { get; set; }

		public string? Language { get; set; }
	}
}