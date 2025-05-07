using BookNest.Dtos;
using BookNest.Entities;
using BookNest.Services.Interface;
using First.Data;
using Microsoft.EntityFrameworkCore;

namespace BookNest.Services
{
    public class AnnouncementService : IAnnouncementService
    {
        private readonly ApplicationDbContext _context;

        public AnnouncementService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAnnouncement(InsertAnnouncementDto announcementDto)
        {
            try
            {
                var newAnnouncement = new Announcement
                {
                    AnnouncementId = Guid.NewGuid(),
                    Title = announcementDto.Title,
                    Message = announcementDto.Message,
                    StartDate = announcementDto.StartDate.ToUniversalTime(),  
                    EndDate = announcementDto.EndDate.ToUniversalTime()       
                };

                await _context.Announcements.AddAsync(newAnnouncement);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task DeleteAnnouncement(Guid id)
        {
            var announcement = await _context.Announcements.FindAsync(id);
            if (announcement != null)
            {
                _context.Announcements.Remove(announcement);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Announcement not found.");
            }
        }


        public async Task<List<GetAllAnnouncementDto>> GetActiveAnnouncements()
        {
            var currentDate = DateTime.UtcNow; // Use UTC time for consistency

            var activeAnnouncements = await _context.Announcements
                .Where(a => a.StartDate <= currentDate && a.EndDate >= currentDate)
                .Select(a => new GetAllAnnouncementDto
                {
                    AnnouncementId = a.AnnouncementId,
                    Title = a.Title,
                    Message = a.Message,
                    StartDate = a.StartDate,
                    EndDate = a.EndDate
                })
                .ToListAsync();

            return activeAnnouncements;
        }


    }
}
