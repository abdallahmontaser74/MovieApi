using MoviesApi.Dtos;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesApi.Models
{
    public class Genre : GenreDto
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public byte Id { get; set; }
    }
}
