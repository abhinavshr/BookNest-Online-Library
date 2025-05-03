using System.ComponentModel.DataAnnotations;

namespace BookNest.Dtos
{
    public class GetAllCartItemDto
    {
        [Key] public Guid CartItemId { get; set; }
        public Guid CartId { get; set; }
        public Guid BookId { get; set; }
        public int Quantity { get; set; }
    }
}
