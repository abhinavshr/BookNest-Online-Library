using BookNest.Dtos;

namespace BookNest.Services.Interface
{
    public interface IBookService
    {
        Task AddBook(InsertBookDto bookDto);
        Task<List<GetAllBookDto>> GetAllBooks();
        Task<GetAllBookDto> GetById(Guid id);
        Task UpdateBook(Guid id, UpdateBookDto bookDto);
        Task DeleteBook(Guid id);
    }
}
