using MovieCatalogAPI.Models;

namespace MovieCatalogAPI.Data
{
    public class MovieRepo : IMovieRepo
    {
        private readonly AppDbContext _context;

        public MovieRepo(AppDbContext context)
        {
            _context = context;
        }

        public void CreateMovie(Movie movie)
        {
            if (movie == null)
            {
                throw new ArgumentNullException(nameof(movie));
            }

            _context.Movies.Add(movie);
        }

        public Movie? GetLastAddedMovie()
        {
            return _context.Movies
                .OrderByDescending(x => x.DateAdded).FirstOrDefault();
        }

        public IEnumerable<Movie> GetMoviesByGenre(string genre)
        {
            return _context.Movies
                .Where(x => x.Genre == genre)
                .OrderBy(x => x.Title);
        }

        public IEnumerable<Movie> GetMoviesByYear(int year)
        {
            return _context.Movies
                .Where(x => x.Year == year)
                .OrderBy(x => x.Title);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
