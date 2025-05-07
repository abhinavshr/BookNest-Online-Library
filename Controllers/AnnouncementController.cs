using BookNest.Dtos;
using BookNest.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BookNest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnnouncementController : ControllerBase
    {
        private readonly IAnnouncementService _announcementService;

        public AnnouncementController(IAnnouncementService announcementService)
        {
            _announcementService = announcementService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddAnnouncement([FromBody] InsertAnnouncementDto announcementDto)
        {
            try
            {
                await _announcementService.AddAnnouncement(announcementDto);
                return Ok(new { message = "Announcement added successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<GetAllAnnouncementDto>>> GetActiveAnnouncements()
        {
            try
            {
                var activeAnnouncements = await _announcementService.GetActiveAnnouncements();

                if (activeAnnouncements == null || !activeAnnouncements.Any())
                {
                    return NotFound(new { message = "No active announcements found." });
                }

                return Ok(activeAnnouncements);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving active announcements.", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnnouncement(Guid id)
        {
            try
            {
                await _announcementService.DeleteAnnouncement(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

    }
}
