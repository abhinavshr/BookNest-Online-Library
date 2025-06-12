using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookNest.Entities
{
    public class CartItem
    {
        [Key] public Guid CartItemId { get; set; } = Guid.NewGuid();

        [ForeignKey(nameof(Cart))] public Guid UserId { get; set; }

        [ForeignKey(nameof(Book))] public Guid BookId { get; set; }

        public int Quantity { get; set; }

        public virtual User User { get; set; }
        public virtual Book Book { get; set; }
    }

}
