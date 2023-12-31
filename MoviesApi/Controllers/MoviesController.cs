﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using MoviesApi.Dtos;
using MoviesApi.Models;
using MoviesApi.Services.Genres;
using MoviesApi.Services.Movies;

namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMoviesService _moviesService;
        private readonly IGenresService _genresService;

        private new List<string> _allowedExtenstion = new List<string> { ".jpg", ".png" };
        private long _maxAllowedPosterSize = 1048576;

        public MoviesController(IMoviesService moviesService, IGenresService genresService)
        {
            _moviesService = moviesService;
            _genresService = genresService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var movies = await _moviesService.GetAll();

            var data = movies.Select(m => new MovieDetailsDto
            {
                Id = m.Id,
                GenreId = m.GenreId,
                GenreName = m.Genre.Name,
                // Poster = m.Poster,
                Rate = m.Rate,
                StoreLine = m.StoreLine,
                Title = m.Title,
                Year = m.Year
            });

            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var movie = await _moviesService.GetById(id);

            if (movie == null)
            {
                return NotFound();
            }

            var dto = new MovieDetailsDto
            {
                Id = movie.Id,
                GenreId = movie.GenreId,
                GenreName = movie.Genre.Name,
                // Poster = movie.Poster,
                Rate = movie.Rate,
                StoreLine = movie.StoreLine,
                Title = movie.Title,
                Year = movie.Year
            };

            return Ok(dto);
        }

        [HttpGet("GetByGenreId")]
        public async Task<IActionResult> GetByGenreIdAsync(byte genreId)
        {
            var movies = await _moviesService.GetAll(genreId);

            var date = movies.Select(m => new MovieDetailsDto
            {
                Id = m.Id,
                GenreId = m.GenreId,
                GenreName = m.Genre.Name,
                // Poster = m.Poster,
                Rate = m.Rate,
                StoreLine = m.StoreLine,
                Title = m.Title,
                Year = m.Year
            });

            return Ok(date);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] MovieCreateDto dto)
        {

            if (!_allowedExtenstion.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
            {
                return BadRequest("Only .png and .jpg images are allowed!");
            }

            if (dto.Poster.Length > _maxAllowedPosterSize)
            {
                return BadRequest("Max allowed size for poster is 1MB!");
            }

            var isValidGenre = await _genresService.IsvalidGenre(dto.GenreId);

            if (!isValidGenre)
            {
                return BadRequest("Invalid genere ID!");
            }

            using var dataStream = new MemoryStream();

            await dto.Poster.CopyToAsync(dataStream);

            var movie = new Movie
            {
                GenreId = dto.GenreId,
                Title = dto.Title,
                Poster = dataStream.ToArray(),
                Rate = dto.Rate,
                StoreLine = dto.StoreLine,
                Year = dto.Year
            };

            await _moviesService.Add(movie);

            return Ok(movie);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromForm] MovieUpdateDto dto)
        {
            var movie = await _moviesService.GetById(id);

            if (movie == null)
            {
                return NotFound($"No movie was found wid ID : {id}");
            }

            var isValidGenre = await _genresService.IsvalidGenre(dto.GenreId);

            if (!isValidGenre)
            {
                return BadRequest("Invalid genere ID!");
            }
            if (dto.Poster != null)
            {
                if (!_allowedExtenstion.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                {
                    return BadRequest("Only .png and .jpg images are allowed!");
                }

                if (dto.Poster.Length > _maxAllowedPosterSize)
                {
                    return BadRequest("Max allowed size for poster is 1MB!");
                }

                using var dataStream = new MemoryStream();

                await dto.Poster.CopyToAsync(dataStream);

                movie.Poster = dataStream.ToArray(); ;

            }

            movie.Title = dto.Title;
            movie.GenreId = dto.GenreId;
            movie.Year = dto.Year;
            movie.StoreLine = dto.StoreLine;
            movie.Rate = dto.Rate;

            _moviesService.Update(movie);

            return Ok(movie);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var movie = await _moviesService.GetById(id);

            if (movie == null)
            {
                return NotFound($"No movie was found wid ID : {id}");
            }

            _moviesService.Delete(movie);

            return Ok(movie);
        }
    }
}
