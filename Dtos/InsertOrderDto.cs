using System;

namespace BookNest.Dtos
{
    public class InsertOrderDto
    {
        public Guid UserId { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal DiscountApplied { get; set; }
        public string ClaimCode { get; set; }
        public string Status { get; set; } // e.g., Pending, Cancelled, Completed
        public DateTime CreatedAt { get; set; }
    }
}
