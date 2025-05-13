using BookNest.Dtos;
using BookNest.Entities;
using BookNest.Services;
using BookNest.Services.Interface;
using First.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookNest.Controllers
{
    [Route("api/book")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;
        private readonly IAuthorService _authorService;
        private readonly IGenreService _genreService;
        private readonly IPublisherService _publisherService;
        private readonly ApplicationDbContext _context;

        public BooksController(IBookService bookService, IAuthorService authorService, IGenreService genreService, IPublisherService publisherService, ApplicationDbContext context)
        {
            _bookService = bookService;
            _authorService = authorService;
            _genreService = genreService;
            _publisherService = publisherService;
            _context = context;
        }

        [HttpPost("addbook")]
        public async Task<IActionResult> AddBook([FromBody] InsertBookDto bookDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    error = "Invalid input data",
                    details = ModelState.Values.SelectMany(v => v.Errors)
                });
            }
            if (bookDto.PublicationDate == default(DateTime) || bookDto.PublicationDate == DateTime.MinValue)
            {
                return BadRequest(new { error = "Publication Date cannot be default or min value." });
            }

            try
            {
                var newBook = new Book
                {
                    Title = bookDto.Title,
                    ISBN = bookDto.ISBN,
                    AuthorId = bookDto.AuthorId,
                    PublisherId = bookDto.PublisherId,
                    GenreId = bookDto.GenreId,
                    Language = bookDto.Language,
                    Description = bookDto.Description,
                    Price = bookDto.Price,
                    Rating = bookDto.Rating,
                    Stock = bookDto.Stock,
                    PhysicalAvailability = bookDto.PhysicalAvailability,
                    PublicationDate = bookDto.PublicationDate,
                    AwardWinners = bookDto.AwardWinners,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _context.Books.Add(newBook);
                await _context.SaveChangesAsync();
                return Ok(new { message = "Book added successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    error = "An error occurred while adding the book.",
                    message = ex.Message,
                    stackTrace = ex.StackTrace 
                });
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<GetAllBookDto>>> GetAllBooks()
        {
            var books = await _bookService.GetAllBooks();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var bookDto = await _bookService.GetById(id);
                return Ok(bookDto);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching the book.", error = ex.Message });
            }
        }

        [HttpPut("updatebook/{id}")]
        public async Task<IActionResult> UpdateBook(Guid id, [FromBody] UpdateBookDto updateBookDto)
        {
            if (updateBookDto == null)
            {
                return BadRequest(new { message = "Invalid book data." });
            }

            try
            {
                await _bookService.UpdateBook(id, updateBookDto);
                return Ok(new { message = "Book updated successfully." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the book.", error = ex.Message });
            }
        }

        [HttpGet("books-published-in-last-week")]
        public async Task<ActionResult<List<GetAllBookDto>>> GetBooksPublishedInLastWeek()
        {
            var books = await _bookService.GetBooksPublishedInLastWeek();

            if (books.Count == 0)
                return NotFound("No books published in the last week.");

            return Ok(books);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(Guid id)
        {
            try
            {
                await _bookService.DeleteBook(id);
                return Ok(new { Message = "Book deleted successfully." });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { Error = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpGet("top-selling")]
        public async Task<ActionResult<GetAllBookDto>> GetTopSellingBook()
        {
            var book = await _bookService.GetTopSellingBook();
            if (book == null)
                return NotFound("No top-selling book found.");

            return Ok(book);
        }

        [HttpGet("books-with-awards")]
        public async Task<ActionResult<List<GetAllBookDto>>> GetBooksWithAwards()
        {
            var booksWithAwards = await _bookService.GetBooksWithAwards();

            if (booksWithAwards.Count == 0)
                return NotFound("No books with awards found.");

            return Ok(booksWithAwards);
        }

        [HttpGet("books-published-in-last-month")]
        public async Task<ActionResult<List<GetAllBookDto>>> GetBooksPublishedInLastMonth()
        {
            var books = await _bookService.GetBooksPublishedInLastMonth();

            if (books.Count == 0)
                return NotFound("No books published in the last month.");

            return Ok(books);
        }

        [HttpGet("comingsoon")]
        public async Task<ActionResult<List<GetAllBookDto>>> GetComingSoonBooks()
        {
            try
            {
                var books = await _bookService.GetComingSoonBooks();
                if (books == null || books.Count == 0)
                {
                    return NotFound("No upcoming books found.");
                }

                return Ok(books);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("details/{id}")]
        public async Task<IActionResult> GetBookById(Guid id)
        {
            var book = await _bookService.GetBookById(id);
            if (book == null)
                return NotFound();

            return Ok(book);
        }

    }
}
