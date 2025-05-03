namespace BookNest.Dtos
{
    public class InsertCartItemDto
    {
        public Guid CartId { get; set; }
        public Guid BookId { get; set; }
        public int Quantity { get; set; }
    }
}
