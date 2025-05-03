using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookNest.Entities
{
    public class Book
    {
        [Key] public Guid BookId { get; set; } = Guid.NewGuid();

        [Required] public string Title { get; set; }

        [Required] public string ISBN { get; set; }

        [ForeignKey(nameof(Author))] public Guid AuthorId { get; set; }

        [ForeignKey(nameof(Publisher))] public Guid PublisherId { get; set; }

        [ForeignKey(nameof(Genre))] public Guid GenreId { get; set; }

        [Required] public string Language { get; set; }

        [ForeignKey(nameof(BookFormat))] public Guid FormatId { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public double Rating { get; set; }

        public int Stock { get; set; }

        public bool PhysicalAvailability { get; set; }

        public DateTime PublicationDate { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }

}
