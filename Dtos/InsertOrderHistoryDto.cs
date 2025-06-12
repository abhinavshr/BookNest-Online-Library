namespace BookNest.Dtos
{
    public class InsertOrderHistoryDto
    {
        public Guid UserId { get; set; }
        public Guid OrderId { get; set; }
        public DateTime CompletedAt { get; set; }
    }
}
