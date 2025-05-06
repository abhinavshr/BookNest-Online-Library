using BookNest.Dtos;
using BookNest.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BookNest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
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

    }

}
