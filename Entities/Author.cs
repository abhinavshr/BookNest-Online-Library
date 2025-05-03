using System.ComponentModel.DataAnnotations;

namespace BookNest.Entities
{
    public class Author
    {
        [Key] public Guid AuthorId { get; set; } = Guid.NewGuid();

        [Required] public string Name { get; set; }

        public string? Biography { get; set; }
    }

}
