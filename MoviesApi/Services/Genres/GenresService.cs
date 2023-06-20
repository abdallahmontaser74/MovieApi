using MoviesApi.Models;
using MoviesApi.Services.Genres.Repositories;

namespace MoviesApi.Services.Genres
{
    public class GenresService : IGenresService
    {
        private readonly IGenresRepository _genresRepository;

        public GenresService(IGenresRepository genresRepository)
        {
            _genresRepository = genresRepository;
        }

        public async Task<Genre> Add(Genre genre)
        {
            return await _genresRepository.Add(genre);
        }

        public Genre Delete(Genre genre)
        {
            return _genresRepository.Delete(genre);
        }

        public async Task<IEnumerable<Genre>> GetAll()
        {
            return await _genresRepository.GetAll();
        }

        public async Task<Genre> GetById(byte id)
        {
            return await _genresRepository.GetById(id);
        }

        public async Task<bool> IsvalidGenre(byte id)
        {
            return await _genresRepository.IsvalidGenre(id);
        }

        public Genre Update(Genre genre)
        {
            return _genresRepository.Update(genre);
        }
    }
}
