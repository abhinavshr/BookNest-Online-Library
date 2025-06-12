using BookNest.Dtos;

namespace BookNest.Services.Interface
{
    public interface ICartService
    {
        void CreateCart(Guid userId);
        GetAllCartDto GetCartByUser(Guid userId);
        void DeleteCart(Guid cartId);
    }
}
