using System;
using System.Text;
using BookNest.Dtos;
using BookNest.Entities;
using BookNest.Services.Interface;
using First.Data;
using Microsoft.AspNetCore.Mvc;
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
            try
            {
                var order = await _context.Orders
                    .Include(o => o.OrderItems)
                    .FirstOrDefaultAsync(o => o.OrderId == id);

                if (order == null)
                {
                    throw new KeyNotFoundException($"Order with ID {id} not found.");
                }

                return order;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving the order.", ex);
            }
        }


        public async Task UpdateOrderAsync(Order order)
        {
            _context.Orders.Update(order); 
            await _context.SaveChangesAsync();
        }

        public async Task<ActionResult> CheckClaimCodeAsync(CheckClaimCodeDto checkClaimCodeDto)
        {
            var order = await GetOrderByIdAsync(checkClaimCodeDto.OrderId);

            if (order == null)
            {
                return new NotFoundObjectResult("Order not found.");
            }

            if (int.TryParse(checkClaimCodeDto.ClaimCode, out int claimCodeParsed))
            {
                if (order.ClaimCode == claimCodeParsed)
                {
                    order.OrderStatus = "Completed";
                    await UpdateOrderAsync(order);
                    return new OkObjectResult("Order status updated to 'Completed'.");
                }
                else
                {
                    return new BadRequestObjectResult("Invalid claim code.");
                }
            }
            else
            {
                return new BadRequestObjectResult("Claim code is not a valid integer.");
            }
        }

        public async Task<List<Order>> GetOrdersByUserIdAsync(Guid userId)
        {
            return await _context.Orders
                                 .Where(o => o.UserId == userId)
                                 .Include(o => o.OrderItems)
                                 .ToListAsync();
        }


    }
}
