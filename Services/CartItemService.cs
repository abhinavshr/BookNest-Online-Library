using BookNest.Dtos;
using BookNest.Entities;
using BookNest.Services.Interface;
using First.Data;
using Microsoft.EntityFrameworkCore;

namespace BookNest.Services
{
    public class CartItemService : ICartItemService
    {
        private readonly ApplicationDbContext _context;

        public CartItemService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddCartItem(InsertCartItemDto itemDto)
        {
            var cartItem = new CartItem
            {
                UserId = itemDto.UserId,
                BookId = itemDto.BookId,
                Quantity = 1
            };

            _context.CartItems.Add(cartItem);
            await _context.SaveChangesAsync();
        }


        public Task<List<GetAllCartItemDto>> GetItemsByCart(Guid cartId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveCartItem(Guid cartItemId)
        {
            throw new NotImplementedException();
        }
    }
}
