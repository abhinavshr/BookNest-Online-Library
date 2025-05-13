using System;
using System.Text;
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
        private readonly EmailService _emailService;

        public OrderService(ApplicationDbContext context, EmailService emailService)
        {
            _context = context;
            _emailService = emailService;
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

            var orderSummary = GenerateOrderSummary(order);
            await _emailService.SendOrderConfirmationEmailAsync(
                dto.UserEmail,
                dto.UserName,
                generatedClaimCode.ToString(),
                orderSummary
            );

            return order;
        }

        private string GenerateOrderSummary(Order order)
        {
            var summary = new StringBuilder();

            summary.AppendLine("----------------------------");
            summary.AppendLine($"Order Date : {order.OrderDate:dd-MMM-yyyy}");
            summary.AppendLine($"Subtotal   : Rs. {order.SubTotal:0.00}");
            summary.AppendLine($"Tax        : Rs. {order.TaxAmount:0.00}");
            summary.AppendLine($"Total      : Rs. {order.TotalAmount:0.00}");

            return summary.ToString();
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
