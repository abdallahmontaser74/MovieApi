using MoviesApi.Dtos;
using System.ComponentModel.DataAnnotations;

namespace MoviesApi.Models
{
    public class Movie : MovieBase
    {
        public int Id { get; set; }
        public Genre Genre { get; set; }
        public byte[] Poster { get; set; }
    }
}
