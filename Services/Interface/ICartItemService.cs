using BookNest.Dtos;

namespace BookNest.Services.Interface
{
    public interface ICartItemService
    {
        Task AddCartItem(InsertCartItemDto itemDto);
        Task<List<GetAllCartItemDto>> GetAllCartItems();
        Task RemoveCartItem(Guid cartItemId);
        Task<List<GetAllCartItemDto>> GetCartItemsByUser(Guid userId);
    }
}
