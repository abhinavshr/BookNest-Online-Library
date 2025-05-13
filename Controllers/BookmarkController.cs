using BookNest.Dtos;
using BookNest.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BookNest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookmarksController : ControllerBase
    {
        private readonly IBookmarkService _bookmarkService;

        public BookmarksController(IBookmarkService bookmarkService)
        {
            _bookmarkService = bookmarkService;
        }

        [HttpPost]
        public async Task<IActionResult> AddBookmark([FromBody] InsertBookmarkDto bookmarkDto)
        {
            try
            {
                Console.WriteLine($"Received request to bookmark book with UserId: {bookmarkDto.UserId}, BookId: {bookmarkDto.BookId}");

                await _bookmarkService.AddBookmark(bookmarkDto);

                Console.WriteLine("Bookmark added successfully.");
                return Ok(new { message = "Bookmark added successfully." });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred in AddBookmark: {ex.Message}");
                return StatusCode(500, new { message = "Internal Server Error. Please try again later." });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBookmark(Guid id)
        {
            await _bookmarkService.DeleteBookmark(id);
            return Ok(new { message = "Bookmark deleted successfully." });
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetBookmarksByUser(Guid userId)
        {
            var bookmarks = await _bookmarkService.GetBookmarksByUser(userId);
            return Ok(bookmarks);
        }
    }
}
