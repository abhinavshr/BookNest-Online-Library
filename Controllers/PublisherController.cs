using BookNest.Dtos;
using BookNest.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookNest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PublisherController : ControllerBase
    {
        private readonly IPublisherService _publisherService;

        public PublisherController(IPublisherService publisherService)
        {
            _publisherService = publisherService;
        }

        [HttpGet]
        public async Task<ActionResult<List<GetAllPublisherDto>>> GetAllPublishers()
        {
            try
            {
                var publishers = await _publisherService.GetAllPublishers();
                return Ok(publishers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePublisher(Guid id)
        {
            try
            {
                await _publisherService.DeletePublisher(id);
                return Ok(new { Message = "Publisher deleted successfully." });
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


        [HttpPost("add")]
        public async Task<IActionResult> AddPublisher([FromBody] InsertPublisherDto publisherDto)
        {
            try
            {
                if (publisherDto == null || string.IsNullOrWhiteSpace(publisherDto.Name) || string.IsNullOrWhiteSpace(publisherDto.Description))
                {
                    return BadRequest("Invalid input data.");
                }

                await _publisherService.AddPublisher(publisherDto);

                return Ok(new { message = "Publisher added successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
