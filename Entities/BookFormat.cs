using System.ComponentModel.DataAnnotations;

namespace BookNest.Entities
{
    public class BookFormat
    {
        [Key] public Guid FormatId { get; set; } = Guid.NewGuid();

        [Required] public string FormatName { get; set; } // Paperback, Hardcover, etc.
    }

}
