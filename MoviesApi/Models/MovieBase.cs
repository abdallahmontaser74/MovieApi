using System.ComponentModel.DataAnnotations;

namespace MoviesApi.Models
{
    public class MovieBase
    {
        public int Year { get; set; }
        public double Rate { get; set; }
        public string StoreLine { get; set; }
        public byte GenreId { get; set; }

        [MaxLength(250)]
        public string Title { get; set; }
    }
}
