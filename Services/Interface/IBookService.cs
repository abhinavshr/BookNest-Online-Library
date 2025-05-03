using BookNest.Dtos;

namespace BookNest.Services.Interface
{
    public interface IBookService
    {
        void AddBook(InsertBookDto bookDto);
        List<GetAllBookDto> GetAllBooks();
        GetAllBookDto GetById(Guid id);
        void UpdateBook(Guid id, UpdateBookDto bookDto);
        void DeleteBook(Guid id);
    }
}
