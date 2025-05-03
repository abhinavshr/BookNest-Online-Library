using System.ComponentModel.DataAnnotations;

namespace BookNest.Entities
{
    public class Publisher
    {
        [Key] public Guid PublisherId { get; set; } = Guid.NewGuid();

        [Required] public string Name { get; set; }

        public string? Description { get; set; }
    }

}
