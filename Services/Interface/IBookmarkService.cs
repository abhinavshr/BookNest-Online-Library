using BookNest.Dtos;

namespace BookNest.Services.Interface
{
    public interface IBookmarkService
    {
        Task AddBookmark(InsertBookmarkDto bookmarkDto);
        Task<List<GetAllBookmarkDto>> GetBookmarksByUser(Guid userId);
        Task DeleteBookmark(Guid id);
    }
}
