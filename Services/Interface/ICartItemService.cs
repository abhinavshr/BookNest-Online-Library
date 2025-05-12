using BookNest.Dtos;

namespace BookNest.Services.Interface
{
    public interface ICartItemService
    {
        Task AddCartItem(InsertCartItemDto itemDto);
        Task<List<GetAllCartItemDto>> GetItemsByCart(Guid cartId);
        Task RemoveCartItem(Guid cartItemId);
    }
}
