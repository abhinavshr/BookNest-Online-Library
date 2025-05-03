using System.ComponentModel.DataAnnotations;

namespace BookNest.Dtos
{
    public class GetAllDiscountDto
    {
        [Key] public Guid DiscountId { get; set; }
        public Guid? BookId { get; set; }
        public string DiscountType { get; set; }
        public decimal Value { get; set; }
        public bool IsOnSale { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
