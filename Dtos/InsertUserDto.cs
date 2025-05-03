namespace BookNest.Dtos
{
    public class InsertUserDto
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }

        public string MembershipID { get; set; }

        public DateTime RegistrationDate { get; set; }

        public int OrderCount { get; set; }

        public bool IsActive { get; set; }
    }
}
