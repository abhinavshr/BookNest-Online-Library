using BookNest.Dtos;

namespace BookNest.Services.Interface
{
    public interface ICartItemService
    {
        void AddCartItem(InsertCartItemDto itemDto);
        List<GetAllCartItemDto> GetItemsByCart(Guid cartId);
        void RemoveCartItem(Guid cartItemId);
    }
}
