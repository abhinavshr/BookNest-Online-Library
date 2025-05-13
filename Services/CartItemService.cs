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


        public async Task<List<GetAllCartItemDto>> GetAllCartItems()
        {
            var cartItems = await _context.CartItems
                .Include(ci => ci.Book)
                    .ThenInclude(b => b.Author)
                .Select(ci => new GetAllCartItemDto
                {
                    CartItemId = ci.CartItemId,
                    UserId = ci.UserId,
                    BookId = ci.BookId,
                    Quantity = ci.Quantity,
                    BookTitle = ci.Book.Title,
                    Price = ci.Book.Price
                })
                .ToListAsync();

            return cartItems;
        }

        public async Task RemoveCartItem(Guid cartItemId)
        {
            var cartItem = await _context.CartItems
                .FirstOrDefaultAsync(ci => ci.CartItemId == cartItemId);

            if (cartItem == null)
            {
                throw new Exception("Cart item not found.");
            }

            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();
        }

        public async Task<List<GetAllCartItemDto>> GetCartItemsByUser(Guid userId)
        {
            var cartItems = await _context.CartItems
                                          .Where(x => x.UserId == userId)
                                          .ToListAsync();

            var cartItemDtos = cartItems.Select(cartItem => new GetAllCartItemDto
            {
                CartItemId = cartItem.CartItemId,
                UserId = cartItem.UserId,
                BookId = cartItem.BookId,
                Quantity = cartItem.Quantity
            }).ToList();

            return cartItemDtos;
        }

    }
}
