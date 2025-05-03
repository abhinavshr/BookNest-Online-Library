using BookNest.Dtos;

namespace BookNest.Services.Interface
{
    public interface IAnnouncementService
    {
        void AddAnnouncement(InsertAnnouncementDto announcementDto);
        List<GetAllAnnouncementDto> GetActiveAnnouncements();
        void DeleteAnnouncement(Guid id);

    }
}
