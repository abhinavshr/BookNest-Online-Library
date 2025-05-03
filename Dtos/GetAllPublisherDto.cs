using System.ComponentModel.DataAnnotations;

namespace BookNest.Dtos
{
    public class GetAllPublisherDto
    {
        [Key] public Guid PublisherId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
