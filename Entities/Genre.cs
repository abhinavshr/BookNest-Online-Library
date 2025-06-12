using System.ComponentModel.DataAnnotations;

namespace BookNest.Entities
{
    public class Genre
    {
        [Key] public Guid GenreId { get; set; } = Guid.NewGuid();

        [Required] public string Name { get; set; }

        public string? Description { get; set; }
    }

}
