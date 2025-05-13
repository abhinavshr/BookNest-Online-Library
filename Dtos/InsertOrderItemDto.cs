namespace BookNest.Dtos
{
    public class InsertOrderItemDto
    {
        public Guid BookId { get; set; }
        public int Quantity { get; set; }
        public decimal Discount { get; set; }
    }
}
