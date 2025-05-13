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

            // Return 201 Created with a route to GET the order by ID
            return CreatedAtAction(nameof(GetOrderById), new { id = createdOrder.OrderId }, createdOrder);
        }

        public async Task<ActionResult<List<Order>>> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
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
    }
}
