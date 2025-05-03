using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookNest.Entities
{
    public class Review
    {
        [Key] public Guid ReviewId { get; set; } = Guid.NewGuid();

        [ForeignKey(nameof(User))] public Guid UserId { get; set; }

        [ForeignKey(nameof(Book))] public Guid BookId { get; set; }

        [Range(1, 5)] public int Rating { get; set; }

        public string? Comment { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

}
