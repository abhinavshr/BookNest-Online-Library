namespace BookNest.Dtos
{
    public class InsertReviewDto
    {
        public Guid UserId { get; set; }

        public Guid BookId { get; set; }

        public double Rating { get; set; }

        public string Comment { get; set; }
    }
}
