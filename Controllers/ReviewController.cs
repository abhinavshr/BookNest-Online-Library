using Microsoft.AspNetCore.Mvc;
using BookNest.Dtos;
using BookNest.Services.Interface;

namespace BookNest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReviewsController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewsController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpPost]
        public async Task<IActionResult> AddReview([FromBody] InsertReviewDto reviewDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _reviewService.AddReview(reviewDto);

                return Ok(new { message = "Review added successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet("book/{bookId}")]
        public async Task<IActionResult> GetReviewsForBook(Guid bookId)
        {
            var reviews = await _reviewService.GetReviewsForBook(bookId);

            if (reviews == null || !reviews.Any())
                return NotFound(new { message = "No reviews found for this book." });

            return Ok(reviews);
        }
    }
}
