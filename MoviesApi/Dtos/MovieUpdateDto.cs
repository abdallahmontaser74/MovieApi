using MoviesApi.Models;

namespace MoviesApi.Dtos
{
    public class MovieUpdateDto : MovieBase
    {
        public IFormFile ?Poster { get; set; }
    }
}
