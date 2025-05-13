namespace BookNest.Dtos
{
    public class CheckClaimCodeDto
    {
        public Guid OrderId { get; set; }
        public string ClaimCode { get; set; }
    }
}
