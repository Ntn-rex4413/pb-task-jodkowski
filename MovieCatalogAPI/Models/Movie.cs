using System.ComponentModel.DataAnnotations;

namespace MovieCatalogAPI.Models
{
    public class Movie
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string? Title { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public  string? Genre { get; set; }

        [Required]
        public DateTime DateAdded { get; set; }
    }
}
