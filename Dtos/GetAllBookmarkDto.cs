using System.ComponentModel.DataAnnotations;

namespace BookNest.Dtos
{
    public class GetAllBookmarkDto
    {
        [Key] public Guid BookmarkId { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public Guid BookId { get; set; }
        public DateTime CreatedAt { get; set; }

        public string Title { get; set; }
        public string AuthorName { get; set; }
        public decimal Price { get; set; }
    }
}
