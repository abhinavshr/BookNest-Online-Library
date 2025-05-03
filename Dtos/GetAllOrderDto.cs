using System.ComponentModel.DataAnnotations;

namespace BookNest.Dtos
{
    public class GetAllOrderDto
    {
        [Key] public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal DiscountApplied { get; set; }
        public string ClaimCode { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? CancelledAt { get; set; }
        public DateTime? CompletedAt { get; set; }
    }
}
