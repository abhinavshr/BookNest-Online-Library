using BookNest.Dtos;
using BookNest.Entities;
using BookNest.Services.Interface;
using First.Data;
using Microsoft.EntityFrameworkCore;

namespace BookNest.Services
{
    public class GenresService : IGenreService
    {
        private readonly ApplicationDbContext _context;

        public GenresService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddGenre(InsertGenreDto genreDto)
        {
            var newGenre = new Genre
            {
                GenreId = Guid.NewGuid(),
                Name = genreDto.Name,
                Description = genreDto.Description
            };

            await _context.Genres.AddAsync(newGenre);
            await _context.SaveChangesAsync();
        }


        public async Task DeleteGenre(Guid id)
        {
            var genre = await _context.Genres.FindAsync(id);
            if (genre == null)
                throw new KeyNotFoundException("Genre not found");

            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();
        }


        public async Task<List<GetAllGenreDto>> GetAllGenres()
        {
            var genres = await _context.Genres
                .Select(g => new GetAllGenreDto
                {
                    GenreId = g.GenreId,
                    Name = g.Name,
                    Description = g.Description
                })
                .ToListAsync();

            return genres;
        }


        public async Task<GetAllGenreDto> GetById(Guid id)
        {
            var genre = await _context.Genres.FindAsync(id);

            if (genre == null)
                throw new KeyNotFoundException("Genre not found.");

            return new GetAllGenreDto
            {
                GenreId = genre.GenreId,
                Name = genre.Name,
                Description = genre.Description
            };
        }


        public async Task UpdateGenre(Guid id, UpdateGenreDto genreDto)
        {
            var genre = await _context.Genres.FindAsync(id);

            if (genre == null)
                throw new KeyNotFoundException("Genre not found.");

            genre.Name = genreDto.Name;
            genre.Description = genreDto.Description;

            _context.Genres.Update(genre);
            await _context.SaveChangesAsync();
        }

    }
}
