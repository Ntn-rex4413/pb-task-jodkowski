using MovieCatalogAPI.Models;

namespace MovieCatalogAPI.Data
{
    public interface IMovieRepo
    {
        void CreateMovie(Movie movie);

        Movie? GetLastAddedMovie();

        IEnumerable<Movie> GetMoviesByYear(int year);

        IEnumerable<Movie> GetMoviesByGenre(string genre);

        bool SaveChanges();
    }
}
