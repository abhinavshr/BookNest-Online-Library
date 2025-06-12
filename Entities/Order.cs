using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BookNest.Entities
{
    public class Order
    {
        [Key]
        public Guid OrderId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        [Column(TypeName = "numeric")]
        public decimal DiscountApplied { get; set; }

        [Required]
        public string OrderDate { get; set; }

        [Required]
        [Column(TypeName = "numeric")]
        public decimal TaxAmount { get; set; }

        [Required]
        [Column(TypeName = "numeric")]
        public decimal SubTotal { get; set; }

        [Required]
        [Column(TypeName = "numeric")]
        public decimal TotalAmount { get; set; }

        [Required]
        public string Payment { get; set; }

        public string? OrderStatus { get; set; }

        public int? ClaimCode { get; set; }

        public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
