using BookNest.Dtos;
using System.ComponentModel.DataAnnotations;

namespace BookNest.Dtos
{

    public class InsertOrderDto
    {
        public Guid UserId { get; set; }
        public decimal DiscountApplied { get; set; }
        public string OrderDate { get; set; } = string.Empty;
        public decimal TaxAmount { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TotalAmount { get; set; }
        public string Payment { get; set; } = string.Empty;
        public string? OrderStatus { get; set; }
        public int? ClaimCode { get; set; }

        public List<InsertOrderItemDto> Items { get; set; } = new();

        public string UserName { get; set; } 
        public string UserEmail { get; set; }
    }
}