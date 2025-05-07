using BookNest.Dtos;
using BookNest.Services.Interface;
using Microsoft.AspNetCore.Mvc;    // Update with the actual namespace where InsertDiscountDto is defined

namespace BookNest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountsController : ControllerBase
    {
        private readonly IDiscountService _discountService; 

        public DiscountsController(IDiscountService discountService)
        {
            _discountService = discountService;
        }

        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddDiscount([FromBody] InsertDiscountDto discountDto)
        {
            if (discountDto == null)
            {
                return BadRequest("Discount data is required.");
            }

            try
            {
                await _discountService.AddDiscount(discountDto);

                return Ok(new { message = "Discount added successfully!" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while adding the discount.");
            }
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllDiscounts()
        {
            try
            {
                var discounts = await _discountService.GetAllDiscounts();
                return Ok(discounts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDiscount(Guid id)
        {
            try
            {
                await _discountService.DeleteDiscount(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(new { error = ex.Message });
            }
        }

    }
}
