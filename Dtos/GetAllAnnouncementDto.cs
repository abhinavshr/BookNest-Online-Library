using System.ComponentModel.DataAnnotations;

namespace BookNest.Dtos
{
    public class GetAllAnnouncementDto
    {
        [Key] public Guid AnnouncementId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
