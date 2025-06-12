using BookNest.Dtos;
using BookNest.Services.Interface;
using First.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace BookNest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;
        private readonly ApplicationDbContext _context;

        public AuthorController(IAuthorService authorService, ApplicationDbContext context)
        {
            _authorService = authorService;
            _context = context;
        }

        [HttpPost("addauthor")]
        public async Task<IActionResult> AddAuthor([FromBody] InsertAuthorDto authorDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { error = "Invalid data." });
            }

            try
            {
                await _authorService.AddAuthor(authorDto);
                return Ok(new { message = "Author added successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("getallauthors")]
        public async Task<IActionResult> GetAllAuthors()
        {
            try
            {
                var authors = await _authorService.GetAllAuthors();
                return Ok(authors);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _authorService.DeleteAuthor(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetAllAuthorDto>> GetAuthorById(Guid id)
        {
            try
            {
                var author = await _context.Authors
                    .FirstOrDefaultAsync(a => a.AuthorId == id);

                if (author == null)
                {
                    return NotFound(new { message = "Author not found." });
                }

                var authorDto = new GetAllAuthorDto
                {
                    AuthorId = author.AuthorId,
                    Name = author.Name,
                    Biography = author.Biography
                };

                return Ok(authorDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAuthor(Guid id, UpdateAuthorDto authorDto)
        {
            try
            {
                var author = await _context.Authors.FindAsync(id);

                if (author == null)
                {
                    return NotFound(new { message = "Author not found." });
                }

                author.Name = authorDto.Name;
                author.Biography = authorDto.Biography;

                _context.Authors.Update(author);
                await _context.SaveChangesAsync();

                return NoContent(); // Successfully updated
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }

}
