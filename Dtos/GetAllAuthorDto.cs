using System.ComponentModel.DataAnnotations;

namespace BookNest.Dtos
{
    public class GetAllAuthorDto
    {
        [Key] public Guid AuthorId { get; set; }
        public string Name { get; set; }
        public string Biography { get; set; }
    }
}
