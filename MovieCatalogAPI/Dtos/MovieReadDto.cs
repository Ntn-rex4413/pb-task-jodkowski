namespace MovieCatalogAPI.Dtos
{
    public class MovieReadDto
    {
        public string? Title { get; set; }

        public int Year { get; set; }

        public  string? Genre { get; set; }

        public string? DateAdded { get; set; }
    }
}
