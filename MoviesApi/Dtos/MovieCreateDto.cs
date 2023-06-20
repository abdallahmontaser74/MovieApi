using MoviesApi.Models;
using System.ComponentModel.DataAnnotations;

namespace MoviesApi.Dtos
{
    public class MovieCreateDto : MovieBase
    {
        public IFormFile Poster { get; set; }
    }
}
