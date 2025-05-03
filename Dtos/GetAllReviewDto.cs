using System.ComponentModel.DataAnnotations;

namespace BookNest.Dtos
{
    public class GetAllReviewDto
    {
        [Key] public Guid ReviewId { get; set; }
        public Guid UserId { get; set; }
        public Guid BookId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
