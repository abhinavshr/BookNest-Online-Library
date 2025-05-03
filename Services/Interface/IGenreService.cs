using BookNest.Dtos;

namespace BookNest.Services.Interface
{
    public interface IGenreService
    {
        void AddGenre(InsertGenreDto genreDto);
        List<GetAllGenreDto> GetAllGenres();
        GetAllGenreDto GetById(Guid id);
        void UpdateGenre(Guid id, UpdateGenreDto genreDto);
        void DeleteGenre(Guid id);
    }
}
