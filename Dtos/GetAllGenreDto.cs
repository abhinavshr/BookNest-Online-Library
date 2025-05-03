using System.ComponentModel.DataAnnotations;

namespace BookNest.Dtos
{
    public class GetAllGenreDto
    {
        [Key] public Guid GenreId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
