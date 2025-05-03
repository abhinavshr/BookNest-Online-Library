using BookNest.Dtos;

namespace BookNest.Services.Interface
{
    public interface IOrderHistoryService
    {
        void RecordOrder(InsertOrderHistoryDto historyDto);
        List<GetAllOrderHistoryDto> GetUserOrderHistory(Guid userId);
    }
}
