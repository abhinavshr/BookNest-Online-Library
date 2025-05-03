using BookNest.Dtos;

namespace BookNest.Services.Interface
{
    public interface IOrderService
    {
        void PlaceOrder(InsertOrderDto orderDto);
        List<GetAllOrderDto> GetOrdersByUser(Guid userId);
        void CancelOrder(Guid orderId);
        void CompleteOrder(Guid orderId);
    }
}
