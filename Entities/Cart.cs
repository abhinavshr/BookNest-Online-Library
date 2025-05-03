using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookNest.Entities
{
    public class Cart
    {
        [Key] public Guid CartId { get; set; } = Guid.NewGuid();

        [ForeignKey(nameof(User))] public Guid UserId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

}
