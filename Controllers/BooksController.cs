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
        public async Task<IActionResult> GetBookById(Guid id)
        {
            try
            {
                var book = await _bookService.GetById(id);
                if (book == null)
                {
                    return NotFound(new { Message = "Book not found." });
                }
                return Ok(book);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(Guid id, [FromBody] UpdateBookDto bookDto)
        {
            try
            {
                await _bookService.UpdateBook(id, bookDto);
                return Ok(new { Message = "Book updated successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(Guid id)
        {
            try
            {
                await _bookService.DeleteBook(id);
                return Ok(new { Message = "Book deleted successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
}
