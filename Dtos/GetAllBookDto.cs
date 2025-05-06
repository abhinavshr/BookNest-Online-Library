using System.ComponentModel.DataAnnotations;
using BookNest.Entities;

namespace BookNest.Dtos
{
    public class GetAllBookDto
    {
        [Key] public Guid BookId { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public Guid AuthorId { get; set; }
        public Guid PublisherId { get; set; }
        public Guid GenreId { get; set; }
        public string Language { get; set; }
        public Guid FormatId { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public double Rating { get; set; }
        public int Stock { get; set; }
        public bool PhysicalAvailability { get; set; }
        public DateTime PublicationDate { get; set; }
    }
}
