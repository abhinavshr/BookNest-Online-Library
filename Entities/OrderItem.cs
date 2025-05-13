using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BookNest.Entities
{
    public class OrderItem
    {
        [Key]
        public Guid OrderItemId { get; set; } = Guid.NewGuid();

        [Required]
        [ForeignKey(nameof(Order))]
        public Guid OrderId { get; set; }

        [Required]
        [ForeignKey(nameof(Book))]
        public Guid BookId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "numeric")]
        public decimal Discount { get; set; }

        [JsonIgnore]
        public virtual Order Order { get; set; }

        [JsonIgnore]
        public virtual Book Book { get; set; }
    }
}
