using BookNest.Dtos;

namespace BookNest.Services.Interface
{
    public interface IOrderItemService
    {
        void AddOrderItem(InsertOrderItemDto itemDto);
        List<GetAllOrderItemDto> GetOrderItems(Guid orderId);
    }
}
