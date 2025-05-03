using BookNest.Dtos;
using BookNest.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BookNest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BooksController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public IActionResult GetAllBooks()
        {
            var books = _bookService.GetAllBooks();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public IActionResult GetBookById(Guid id)
        {
            var book = _bookService.GetById(id);
            return Ok(book);
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] InsertBookDto dto)
        {
            _bookService.AddBook(dto);
            return Ok("Book added successfully.");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(Guid id, [FromBody] UpdateBookDto dto)
        {
            _bookService.UpdateBook(id, dto);
            return Ok("Book updated successfully.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(Guid id)
        {
            _bookService.DeleteBook(id);
            return Ok("Book deleted successfully.");
        }
    }
}
