using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookNest.Entities
{
    public class OrderItem
    {
        [Key] public Guid OrderItemId { get; set; } = Guid.NewGuid();

        [ForeignKey(nameof(Order))] public Guid OrderId { get; set; }

        [ForeignKey(nameof(Book))] public Guid BookId { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal Discount { get; set; }
    }

}
