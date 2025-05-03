using BookNest.Dtos;

namespace BookNest.Services.Interface
{
    public interface IDiscountService
    {
        void AddDiscount(InsertDiscountDto discountDto);
        List<GetAllDiscountDto> GetAllDiscounts();
        void DeleteDiscount(Guid id);
    }
}
