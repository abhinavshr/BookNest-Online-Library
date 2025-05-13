using System;
using BookNest.Dtos;
using BookNest.Entities;
using BookNest.Services.Interface;
using First.Data;
using Microsoft.EntityFrameworkCore;

namespace BookNest.Services
{
    public class OrderService : IOrderService
    {

        private readonly ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Order> CreateOrderAsync(InsertOrderDto dto)
        {
            var random = new Random();
            int generatedClaimCode = random.Next(1000, 10000);

            var order = new Order
            {
                OrderId = Guid.NewGuid(),
                UserId = dto.UserId,
                DiscountApplied = dto.DiscountApplied,
                OrderDate = dto.OrderDate,
                TaxAmount = dto.TaxAmount,
                SubTotal = dto.SubTotal,
                TotalAmount = dto.TotalAmount,
                Payment = dto.Payment,
                OrderStatus = dto.OrderStatus,
                ClaimCode = generatedClaimCode,
                OrderItems = dto.Items.Select(item => new OrderItem
                {
                    OrderItemId = Guid.NewGuid(),
                    BookId = item.BookId,
                    Quantity = 1,
                    Discount = item.Discount
                }).ToList()
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .ToListAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(Guid id)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.OrderId == id);
        }
    }
}
