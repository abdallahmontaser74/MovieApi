﻿using MoviesApi.Models;

namespace MoviesApi.Services.Genres.Repositories
{
    public interface IGenresRepository
    {
        public Task<IEnumerable<Genre>> GetAll();
        public Task<Genre> GetById(byte id);
        public Task<Genre> Add(Genre genre);
        public Genre Update(Genre genre);
        public Genre Delete(Genre genre);
        public Task<bool> IsvalidGenre(byte id);
    }
}
