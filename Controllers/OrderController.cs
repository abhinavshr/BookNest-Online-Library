using Microsoft.AspNetCore.Mvc;
using BookNest.Dtos;
using BookNest.Entities;
using System.Threading.Tasks;
using First.Data;
using Microsoft.EntityFrameworkCore;
using BookNest.Services.Interface;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace BookNest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IOrderService _orderService;

        public OrdersController(ApplicationDbContext context, IOrderService orderService)
        {
            _context = context;
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder([FromBody] InsertOrderDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdOrder = await _orderService.CreateOrderAsync(dto);

            return CreatedAtAction(nameof(GetOrderById), new { id = createdOrder.OrderId }, createdOrder);
        }


        [HttpGet]
        public async Task<ActionResult<List<Order>>> GetAllOrders()
        {
            var orders = await _context.Orders
                .Include(o => o.OrderItems)
                .ToListAsync();

            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrderById(Guid id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
                return NotFound();

            return Ok(order);
        }

        [HttpPost("check-claim-code")]
        public async Task<ActionResult> CheckClaimCodeAsync([FromBody] CheckClaimCodeDto checkClaimCodeDto)
        {
            var order = await _orderService.GetOrderByIdAsync(checkClaimCodeDto.OrderId);

            if (order == null)
            {
                return NotFound(new { message = "Order not found." });
            }

            if (int.TryParse(checkClaimCodeDto.ClaimCode, out int claimCodeParsed))
            {
                if (order.ClaimCode == claimCodeParsed)
                {
                    order.OrderStatus = "Completed";
                    await _orderService.UpdateOrderAsync(order);
                    return Ok(new { message = "Order status updated to 'Completed'." });
                }
                else
                {
                    return BadRequest(new { message = "Invalid claim code." });
                }
            }
            else
            {
                return BadRequest(new { message = "Claim code is not a valid integer." });
            }
        }
    }
}
