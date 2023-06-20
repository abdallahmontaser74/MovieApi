using MoviesApi.Models;
using MoviesApi.Services.Genres.Repositories;
using MoviesApi.Services.Movies.Repositories;

namespace MoviesApi.Services.Movies
{
    public class MoviesService : IMoviesService
    {
        private readonly IMoviesRepository _moviesRepository;

        public MoviesService(IMoviesRepository moviesRepository)
        {
            _moviesRepository = moviesRepository;
        }

        public async Task<Movie> Add(Movie movie)
        {
            return await _moviesRepository.Add(movie);
        }

        public Movie Delete(Movie movie)
        {
            return _moviesRepository.Delete(movie);
        }

        public async Task<IEnumerable<Movie>> GetAll(byte genreId = 0)
        {
            return await _moviesRepository.GetAll(genreId);
        }

        public async Task<Movie> GetById(int id)
        {
            return await _moviesRepository.GetById(id); 
        }

        public Movie Update(Movie movie)
        {
           return _moviesRepository.Update(movie);
        }
    }
}
