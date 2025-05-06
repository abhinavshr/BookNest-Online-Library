using System.ComponentModel.DataAnnotations;

namespace BookNest.Entities
{
    public class Announcement
    {
        [Key] public Guid AnnouncementId { get; set; } = Guid.NewGuid();

        public string Title { get; set; }

        public string Message { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

    }

}
