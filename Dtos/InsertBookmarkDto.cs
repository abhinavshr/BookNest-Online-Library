namespace BookNest.Dtos
{
    public class InsertBookmarkDto
    {
        public Guid UserId { get; set; }
        public Guid BookId { get; set; }
    }
}
