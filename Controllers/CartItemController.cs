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

        [HttpGet]
        public async Task<ActionResult<List<GetAllCartItemDto>>> GetAllCartItems()
        {
            var cartItems = await _cartItemService.GetAllCartItems();

            if (cartItems == null || !cartItems.Any())
            {
                return NotFound("No cart items found.");
            }

            return Ok(cartItems);
        }

        [HttpDelete("{cartItemId}")]
        public async Task<ActionResult> RemoveCartItem(Guid cartItemId)
        {
            try
            {
                await _cartItemService.RemoveCartItem(cartItemId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message); 
            }
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<GetAllCartItemDto>>> GetCartItemsByUser(Guid userId)
        {
            try
            {
                var cartItems = await _cartItemService.GetCartItemsByUser(userId);

                if (cartItems == null || !cartItems.Any())
                {
                    return NotFound(new { message = "No cart items found for the user." });
                }

                return Ok(cartItems);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Internal server error: {ex.Message}" });
            }
        }

        [HttpDelete("user/{userId}")]
        public async Task<IActionResult> RemoveAllCartItemsByUser(Guid userId)
        {
            await _cartItemService.RemoveAllCartItemsByUser(userId);
            return NoContent();
        }

    }
}
