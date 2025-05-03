using System.ComponentModel.DataAnnotations;

namespace BookNest.Dtos
{
    public class GetAllOrderHistoryDto
    {
        [Key] public Guid HistoryId { get; set; }
        public Guid UserId { get; set; }
        public Guid OrderId { get; set; }
        public DateTime CompletedAt { get; set; }
    }
}
