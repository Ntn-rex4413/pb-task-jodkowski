using AutoMapper;
using MovieCatalogAPI.Dtos;
using MovieCatalogAPI.Models;

namespace MovieCatalogAPI.Profiles
{
    public class MoviesProfile : Profile
    {
        public MoviesProfile()
        {
            CreateMap<Movie, MovieReadDto>()
                .ForMember(dest => dest.DateAdded, 
                    opt => opt.MapFrom(src => src.DateAdded.ToString("dd/mm/yyyy HH:mm:ss")));

            CreateMap<MovieCreateDto, Movie>()
                .ForMember(dest => dest.Genre,
                    opt => opt.MapFrom(src => src.Genre!.ToUpper()));
        }
    }
}
