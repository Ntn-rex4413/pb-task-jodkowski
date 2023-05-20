using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MovieCatalogAPI.Data;
using MovieCatalogAPI.Dtos;
using MovieCatalogAPI.Models;

namespace MovieCatalogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieRepo _repository;
        private readonly IMapper _mapper;

        public MoviesController(IMovieRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpPost]
        public ActionResult<MovieReadDto> CreateMovie(MovieCreateDto movieCreateDto)
        {
            var movie = _mapper.Map<Movie>(movieCreateDto);

            movie.DateAdded = DateTime.Now;

            _repository.CreateMovie(movie);

            _repository.SaveChanges();

            var movieReadDto = _mapper.Map<MovieReadDto>(movie);

            return CreatedAtAction(nameof(GetLastAddedMovie), movieReadDto);
        }

        [HttpGet]
        public ActionResult<MovieReadDto> GetLastAddedMovie()
        {
            var movie = _repository.GetLastAddedMovie();

            if (movie == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<MovieReadDto>(movie));
        }

        [HttpGet("{year:int}")]
        public ActionResult<IEnumerable<MovieReadDto>> GetMoviesByYear(int year)
        {
            var movies = _repository.GetMoviesByYear(year);

            return Ok(_mapper.Map<IEnumerable<MovieReadDto>>(movies));
        }

        [HttpGet("{genre:alpha}")]
        public ActionResult<IEnumerable<MovieReadDto>> GetMoviesByGenre(string genre)
        {
            var movies = _repository.GetMoviesByGenre(genre.ToUpper());

            return Ok(_mapper.Map<IEnumerable<MovieReadDto>>(movies));
        }
    }
}