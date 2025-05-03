using BookNest.Dtos;

namespace BookNest.Services.Interface
{
    public interface IAuthorService
    {
        void AddAuthor(InsertAuthorDto authorDto);
        List<GetAllAuthorDto> GetAllAuthors();
        GetAllAuthorDto GetById(Guid id);
        void UpdateAuthor(Guid id, UpdateAuthorDto authorDto);
        void DeleteAuthor(Guid id);
    }
}
