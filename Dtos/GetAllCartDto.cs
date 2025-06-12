using System.ComponentModel.DataAnnotations;

namespace BookNest.Dtos
{
    public class GetAllCartDto
    {
        [Key] public Guid CartId { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
