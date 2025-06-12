using System.ComponentModel.DataAnnotations;

namespace BookNest.Dtos
{
    public class InsertAuthorDto
    {
        [Required]
        public string Name { get; set; }

        public string Biography { get; set; }
    }
}
