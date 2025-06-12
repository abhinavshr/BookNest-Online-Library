using System.ComponentModel.DataAnnotations;
using BookNest.Entities;

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

        public string UserName { get; set; }
    }
}
