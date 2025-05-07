using BookNest.Dtos;
using BookNest.Entities;
using BookNest.Services.Interface;
using First.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class GenresController : ControllerBase
{
    private readonly IGenreService _genreService;
    private readonly ApplicationDbContext _context;

    public GenresController(IGenreService genreService, ApplicationDbContext context)
    {
        _genreService = genreService;
        _context = context;
    }

    [HttpPost("add")]
    public async Task<IActionResult> AddGenre([FromBody] InsertGenreDto genreDto)
    {
        try
        {
            if (genreDto == null || string.IsNullOrWhiteSpace(genreDto.Name) || string.IsNullOrWhiteSpace(genreDto.Description))
            {
                return BadRequest("Invalid input data.");
            }

            var newGenre = new Genre
            {
                GenreId = Guid.NewGuid(),
                Name = genreDto.Name,
                Description = genreDto.Description
            };

            await _context.Genres.AddAsync(newGenre);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Genre added successfully!" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpGet]
    public async Task<ActionResult<List<GetAllGenreDto>>> GetAllGenres()
    {
        var genres = await _genreService.GetAllGenres();
        return Ok(genres);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await _genreService.DeleteGenre(id);
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


}
