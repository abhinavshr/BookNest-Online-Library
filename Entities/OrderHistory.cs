using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookNest.Entities
{
    public class OrderHistory
    {
        [Key] public Guid HistoryId { get; set; } = Guid.NewGuid();

        [ForeignKey(nameof(User))] public Guid UserId { get; set; }

        [ForeignKey(nameof(Order))] public Guid OrderId { get; set; }

        public DateTime CompletedAt { get; set; } = DateTime.UtcNow;
    }

}
