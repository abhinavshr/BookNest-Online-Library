using BookNest.Dtos;
using BookNest.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookNest.Services.Interface
{
    public interface IOrderService
    {
        Task<Order> CreateOrderAsync(InsertOrderDto dto);
        Task<List<Order>> GetAllOrdersAsync();
        Task<Order?> GetOrderByIdAsync(Guid id);
        Task<ActionResult> CheckClaimCodeAsync(CheckClaimCodeDto checkClaimCodeDto);
        Task UpdateOrderAsync(Order order);
        Task<List<Order>> GetOrdersByUserIdAsync(Guid userId);
    }
}
