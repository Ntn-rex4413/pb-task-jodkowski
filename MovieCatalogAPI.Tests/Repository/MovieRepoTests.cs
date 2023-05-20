using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MovieCatalogAPI.Data;
using MovieCatalogAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCatalogAPI.Tests.Repository
{
    public class MovieRepoTests
    {
        [Fact]
        public void CreateMovie_Should_Add_Movie()
        {
            // Arrange
            var helper = new TestPersistenceHelper();

            var repository = helper.GetInMemoryRepository();

            var movie = new Movie
            {
                Title = "TestMovie",
                Year = 2010,
                Genre = "Action",
                DateAdded = DateTime.Now,
            };

            // Act
            repository.CreateMovie(movie);
            repository.SaveChanges();

            // Assert
            Assert.True(helper.Context.Movies.Contains(movie));
        }

        [Fact]
        public void GetLastAddedMovie_Should_Return_Null_When_No_Movies_In_Database()
        {
            // Arrange
            var helper = new TestPersistenceHelper();

            var repository = helper.GetInMemoryRepository();

            // Act
            var result = repository.GetLastAddedMovie();

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetLastAddedMovie_Should_Return_Movie_With_Newest_DateAdded()
        {
            // Arrange
            var helper = new TestPersistenceHelper();

            var repository = helper.GetInMemoryRepository();

            var movies = new List<Movie>
            {
                new Movie { Title = "TestMovie1", Year = 2010, Genre = "ACTION", DateAdded = DateTime.Now.AddDays(2) },
                new Movie { Title = "TestMovie1", Year = 2010, Genre = "ACTION", DateAdded = DateTime.Now.AddMonths(3) },
                new Movie { Title = "TestMovie1", Year = 2010, Genre = "ACTION", DateAdded = DateTime.Now.AddMinutes(40) },
                new Movie { Title = "TestMovie1", Year = 2010, Genre = "ACTION", DateAdded = DateTime.Now },
            };

            helper.Context.Movies.AddRange(movies);
            helper.Context.SaveChanges();

            var newestDate = helper.Context.Movies.OrderByDescending(x => x.DateAdded).First().DateAdded;

            // Act
            var result = repository.GetLastAddedMovie();

            // Assert
            Assert.IsType<Movie>(result);
            Assert.Equal(newestDate, result?.DateAdded);
        }

        [Fact]
        public void GetMoviesByGenre_Should_Return_Empty_Collection_When_No_Movies_With_Given_Genre()
        {
            // Arrange
            var helper = new TestPersistenceHelper();

            var repository = helper.GetInMemoryRepository();

            var movies = new List<Movie>
            {
                new Movie { Title = "TestMovie1", Year = 2010, Genre = "ACTION", DateAdded = DateTime.Now.AddDays(2) },
                new Movie { Title = "TestMovie1", Year = 2010, Genre = "DOCUMENTARY", DateAdded = DateTime.Now.AddMonths(3) },
                new Movie { Title = "TestMovie1", Year = 2010, Genre = "COMEDY", DateAdded = DateTime.Now.AddMinutes(40) },
                new Movie { Title = "TestMovie1", Year = 2010, Genre = "ACTION", DateAdded = DateTime.Now },
            };

            helper.Context.Movies.AddRange(movies);
            helper.Context.SaveChanges();

            // Act
            var result = repository.GetMoviesByGenre("nonexistentCategory");

            // Assert
            Assert.NotNull(result);
            Assert.True(typeof(IEnumerable<Movie>).IsAssignableFrom(result.GetType()));
            Assert.False(result.Any());
        }

        [Theory]
        [InlineData("ACTION", 2)]
        [InlineData("COMEDY", 1)]
        [InlineData("SCI-FI", 0)]
        public void GetMoviesByGenre_Should_Return_All_Movies_Matching_Given_Genre(string genre, int matchingMovies)
        {
            // Arrange
            var helper = new TestPersistenceHelper();

            var repository = helper.GetInMemoryRepository();

            var movies = new List<Movie>
            {
                new Movie { Title = "TestMovie1", Year = 2010, Genre = "ACTION", DateAdded = DateTime.Now.AddDays(2) },
                new Movie { Title = "TestMovie1", Year = 2010, Genre = "DOCUMENTARY", DateAdded = DateTime.Now.AddMonths(3) },
                new Movie { Title = "TestMovie1", Year = 2010, Genre = "COMEDY", DateAdded = DateTime.Now.AddMinutes(40) },
                new Movie { Title = "TestMovie1", Year = 2010, Genre = "ACTION", DateAdded = DateTime.Now },
            };

            helper.Context.Movies.AddRange(movies);
            helper.Context.SaveChanges();

            // Act
            var result = repository.GetMoviesByGenre(genre);

            // Assert
            Assert.Equal(matchingMovies, result.Count());
            Assert.All(result, x => Assert.Equal(x.Genre, genre));
        }

        [Fact]
        public void GetMoviesByYear_Should_Return_Empty_Collection_When_No_Movies_With_Given_Year()
        {
            // Arrange
            var helper = new TestPersistenceHelper();

            var repository = helper.GetInMemoryRepository();

            var movies = new List<Movie>
            {
                new Movie { Title = "TestMovie1", Year = 2010, Genre = "ACTION", DateAdded = DateTime.Now.AddDays(2) },
                new Movie { Title = "TestMovie1", Year = 2012, Genre = "DOCUMENTARY", DateAdded = DateTime.Now.AddMonths(3) },
                new Movie { Title = "TestMovie1", Year = 2010, Genre = "COMEDY", DateAdded = DateTime.Now.AddMinutes(40) },
                new Movie { Title = "TestMovie1", Year = 2019, Genre = "ACTION", DateAdded = DateTime.Now },
            };

            helper.Context.Movies.AddRange(movies);
            helper.Context.SaveChanges();

            // Act
            var result = repository.GetMoviesByYear(00000);

            // Assert
            Assert.NotNull(result);
            Assert.True(typeof(IEnumerable<Movie>).IsAssignableFrom(result.GetType()));
            Assert.False(result.Any());
        }

        [Theory]
        [InlineData(2010, 2)]
        [InlineData(2023, 1)]
        [InlineData(2040, 0)]
        public void GetMoviesByYear_Should_Return_All_Movies_Matching_Given_Year(int year, int matchingMovies)
        {
            // Arrange
            var helper = new TestPersistenceHelper();

            var repository = helper.GetInMemoryRepository();

            var movies = new List<Movie>
            {
                new Movie { Title = "TestMovie1", Year = 2010, Genre = "ACTION", DateAdded = DateTime.Now.AddDays(2) },
                new Movie { Title = "TestMovie1", Year = 2023, Genre = "DOCUMENTARY", DateAdded = DateTime.Now.AddMonths(3) },
                new Movie { Title = "TestMovie1", Year = 2010, Genre = "COMEDY", DateAdded = DateTime.Now.AddMinutes(40) },
                new Movie { Title = "TestMovie1", Year = 2019, Genre = "ACTION", DateAdded = DateTime.Now },
            };

            helper.Context.Movies.AddRange(movies);
            helper.Context.SaveChanges();

            // Act
            var result = repository.GetMoviesByYear(year);

            // Assert
            Assert.Equal(matchingMovies, result.Count());
            Assert.All(result, x => Assert.Equal(x.Year, year));
        }
    }
}
