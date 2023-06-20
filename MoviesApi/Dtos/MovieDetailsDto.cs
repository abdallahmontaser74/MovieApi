using MoviesApi.Models;
using System.ComponentModel.DataAnnotations;

namespace MoviesApi.Dtos
{
    public class MovieDetailsDto : MovieBase
    {
        public int Id { get; set; }
        public byte[] Poster { get; set; }
        public string GenreName { get; set; }
    }
}
