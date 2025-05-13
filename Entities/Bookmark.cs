using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookNest.Entities
{
    public class Bookmark
    {
        [Key] public Guid BookmarkId { get; set; } = Guid.NewGuid();

        [ForeignKey(nameof(User))] public Guid UserId { get; set; }

        [ForeignKey(nameof(Book))] public Guid BookId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Book Book { get; set; }
    }

}
