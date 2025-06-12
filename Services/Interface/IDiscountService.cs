using BookNest.Dtos;

namespace BookNest.Services.Interface
{
    public interface IDiscountService
    {
        Task AddDiscount(InsertDiscountDto discountDto);
        Task<List<GetAllDiscountDto>> GetAllDiscounts();
        Task DeleteDiscount(Guid id);
    }
}
