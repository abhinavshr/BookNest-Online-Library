using System.ComponentModel.DataAnnotations;

namespace BookNest.Entities
{
    public class User
    {
        [Key] public Guid UserId { get; set; } = Guid.NewGuid();

        [Required] public string Name { get; set; }

        [Required, EmailAddress] public string Email { get; set; }

        [Required] public string PasswordHash { get; set; }

        [Required]
        public string Role { get; set; }

        public string? MembershipID { get; set; }

        public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;

        public int OrderCount { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
