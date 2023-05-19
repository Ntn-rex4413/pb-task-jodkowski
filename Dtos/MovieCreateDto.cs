using System.ComponentModel.DataAnnotations;

namespace MovieCatalogAPI.Dtos
{
    public class MovieCreateDto
    {
        [Required]
        public string? Title { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public  string? Genre { get; set; }
    }
}
