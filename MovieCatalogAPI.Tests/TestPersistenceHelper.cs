using Microsoft.EntityFrameworkCore;
using MovieCatalogAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieCatalogAPI.Tests
{
    public class TestPersistenceHelper
    {
        public readonly AppDbContext Context;

        public TestPersistenceHelper()
        {
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            builder.UseInMemoryDatabase("TestMovieDatabase");

            var dbContextOptions = builder.Options;
            Context = new AppDbContext(dbContextOptions);
            Context.Database.EnsureDeleted();
            Context.Database.EnsureCreated();
        }

        public IMovieRepo GetInMemoryRepository()
        {
            return new MovieRepo(Context);
        }
    }
}
