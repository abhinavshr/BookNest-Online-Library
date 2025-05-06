using BookNest.Dtos;
using BookNest.Entities;
using BookNest.Services.Interface;
using First.Data;
using Microsoft.EntityFrameworkCore;

namespace BookNest.Services
{
    public class DiscountService : IDiscountService
    {

        private readonly ApplicationDbContext _context;

        public DiscountService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddDiscount(InsertDiscountDto discountDto)
        {
            if (discountDto == null)
            {
                throw new ArgumentNullException(nameof(discountDto), "Discount data is required.");
            }

            var discount = new Discount
            {
                BookId = discountDto.BookId,
                DiscountType = discountDto.DiscountType,
                Value = discountDto.Value,
                IsOnSale = discountDto.IsOnSale,
                StartDate = discountDto.StartDate.ToUniversalTime(),
                EndDate = discountDto.EndDate.ToUniversalTime()
            };

            try
            {
                await _context.Discounts.AddAsync(discount);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while adding the discount.", ex);
            }
        }


        public Task DeleteDiscount(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<GetAllDiscountDto>> GetAllDiscounts()
        {
            var discounts = await _context.Discounts
                .Select(d => new GetAllDiscountDto
                {
                    BookId = d.BookId,
                    DiscountType = d.DiscountType,
                    Value = d.Value,
                    IsOnSale = d.IsOnSale,
                    StartDate = d.StartDate,
                    EndDate = d.EndDate
                })
                .ToListAsync();

            return discounts;
        }
    }
}
