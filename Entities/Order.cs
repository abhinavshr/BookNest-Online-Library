using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookNest.Entities
{
    public class Order
    {
        [Key] public Guid OrderId { get; set; } = Guid.NewGuid();

        [ForeignKey(nameof(User))] public Guid UserId { get; set; }

        public decimal TotalPrice { get; set; }

        public decimal DiscountApplied { get; set; }

        public string ClaimCode { get; set; }

        public string Status { get; set; } = "Pending"; // Pending, Cancelled, Completed

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? CancelledAt { get; set; }

        public DateTime? CompletedAt { get; set; }
    }

}
