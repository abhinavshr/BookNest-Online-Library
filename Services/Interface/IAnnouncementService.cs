using BookNest.Dtos;

namespace BookNest.Services.Interface
{
    public interface IAnnouncementService
    {
        Task AddAnnouncement(InsertAnnouncementDto announcementDto);
        Task<List<GetAllAnnouncementDto>> GetActiveAnnouncements();
        Task DeleteAnnouncement(Guid id);

    }
}
