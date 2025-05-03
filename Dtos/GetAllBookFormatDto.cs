using System.ComponentModel.DataAnnotations;

namespace BookNest.Dtos
{
    public class GetAllBookFormatDto
    {
        [Key] public Guid FormatId { get; set; }
        public string FormatName { get; set; }
    }
}
