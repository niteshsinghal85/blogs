using AwesomeMovie.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AwesomeMovie.Data
{
	public class AppDbContext : DbContext
	{
        public DbSet<Movie> Movies { get; set; } = null!;

        public string DbPath { get; set; }

        public AppDbContext()
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            DbPath = Path.Join(path, "awesomemovie.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");

        public void MigrateAndCreateData()
        {
            Database.Migrate();

            if (!Movies.Any())
            {
                Movies.Add(new Movie
                {
                    Title = "Inception",
                    Language = "English",
                    Rating = 9.8f,
                    ReleaseDate = new DateTime(2010,07,01)
                });
                Movies.Add(new Movie
                {
                    Title = "Hungama",
                    Language = "Hindi",
                    Rating = 8.7f,
                    ReleaseDate = new DateTime(2003, 08, 01)
                });
                Movies.Add(new Movie
                {
                    Title = "Life Is Beautiful",
                    Language = "Italian",
                    Rating = 9.8f,
                    ReleaseDate = new DateTime(1997, 12, 10)
                });
                Movies.Add(new Movie
                {
                    Title = "Tenet",
                    Language = "English",
                    Rating = 8.9f,
                    ReleaseDate = new DateTime(2020, 08, 026)
                });
                Movies.Add(new Movie
                {
                    Title = "Shershaah",
                    Language = "Hindi",
                    Rating = 9.3f,
                    ReleaseDate = new DateTime(2021, 07, 12)
                });

                SaveChanges();
            }
        }
    }
}