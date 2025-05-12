using Microsoft.AspNetCore.Mvc;
using BookNest.Entities;
using BookNest.Services;
using System.Threading.Tasks;
using BookNest.Dtos;
using BookNest.Services.Interface;

namespace BookNest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemController : ControllerBase
    {
        private readonly ICartItemService _cartItemService;

        public CartItemController(ICartItemService cartItemService)
        {
            _cartItemService = cartItemService;
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] InsertCartItemDto itemDto)
        {
            if (itemDto == null || itemDto.UserId == Guid.Empty || itemDto.BookId == Guid.Empty)
            {
                return BadRequest(new { message = "Invalid data." });
            }

            try
            {
                Console.WriteLine($"Received Book ID: {itemDto.BookId}, User ID: {itemDto.UserId}");

                await _cartItemService.AddCartItem(itemDto);

                return Ok(new { message = "Item added to cart successfully." });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, new { message = $"Internal server error: {ex.Message}" });
            }
        }

    }
}
