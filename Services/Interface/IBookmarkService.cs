using BookNest.Dtos;

namespace BookNest.Services.Interface
{
    public interface IBookmarkService
    {
        void AddBookmark(InsertBookmarkDto bookmarkDto);
        List<GetAllBookmarkDto> GetBookmarksByUser(Guid userId);
        void DeleteBookmark(Guid id);
    }
}
