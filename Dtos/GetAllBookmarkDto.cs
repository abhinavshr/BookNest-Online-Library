using System.ComponentModel.DataAnnotations;

namespace BookNest.Dtos
{
    public class GetAllBookmarkDto
    {
        [Key] public Guid BookmarkId { get; set; }
        public Guid UserId { get; set; }
        public Guid BookId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
