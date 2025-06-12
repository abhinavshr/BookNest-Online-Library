using BookNest.Data;
using BookNest.Dtos;
using BookNest.Entities;
using BookNest.Services.Interface;
using First.Data;
using Microsoft.EntityFrameworkCore;

namespace BookNest.Services
{
    public class BookmarkService : IBookmarkService
    {
        private readonly ApplicationDbContext _context;

        public BookmarkService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddBookmark(InsertBookmarkDto bookmarkDto)
        {
            try
            {
                Console.WriteLine($"Checking if bookmark already exists for UserId: {bookmarkDto.UserId}, BookId: {bookmarkDto.BookId}");

                var existingBookmark = await _context.Bookmarks
                    .FirstOrDefaultAsync(b => b.UserId == bookmarkDto.UserId && b.BookId == bookmarkDto.BookId);

                if (existingBookmark != null)
                {
                    Console.WriteLine("Bookmark already exists.");
                    throw new InvalidOperationException("This book is already bookmarked.");
                }

                Console.WriteLine("Adding new bookmark to the database.");

                var newBookmark = new Bookmark
                {
                    UserId = bookmarkDto.UserId,
                    BookId = bookmarkDto.BookId
                };

                _context.Bookmarks.Add(newBookmark);
                await _context.SaveChangesAsync();

                Console.WriteLine("New bookmark added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred in AddBookmark service: {ex.Message}");
                throw;
            }
        }

        public async Task DeleteBookmark(Guid id)
        {
            var bookmark = await _context.Bookmarks.FindAsync(id);
            if (bookmark != null)
            {
                _context.Bookmarks.Remove(bookmark);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<GetAllBookmarkDto>> GetBookmarksByUser(Guid userId)
        {
            var bookmarks = await _context.Bookmarks
                .Where(b => b.UserId == userId)
                .Include(b => b.Book)
                    .ThenInclude(book => book.Author)
                .Select(b => new GetAllBookmarkDto
                {
                    BookmarkId = b.BookmarkId,
                    UserId = b.UserId,
                    BookId = b.BookId,
                    CreatedAt = b.CreatedAt,
                    Title = b.Book.Title,
                    AuthorName = b.Book.Author.Name,
                    Price = b.Book.Price
                })
                .ToListAsync();

            Console.WriteLine("Fetched Bookmarks: " + string.Join(", ", bookmarks.Select(b => b.BookmarkId.ToString())));

            return bookmarks;
        }



    }
}
