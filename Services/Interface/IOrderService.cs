using BookNest.Dtos;
using BookNest.Entities;
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
    }
}
