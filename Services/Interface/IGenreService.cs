using BookNest.Dtos;

namespace BookNest.Services.Interface
{
    public interface IGenreService
    {
        Task AddGenre(InsertGenreDto genreDto);
        Task<List<GetAllGenreDto>> GetAllGenres();
        Task<GetAllGenreDto> GetById(Guid id);
        Task UpdateGenre(Guid id, UpdateGenreDto genreDto);
        Task DeleteGenre(Guid id);
    }
}
