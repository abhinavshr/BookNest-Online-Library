using BookNest.Dtos;
using BookNest.Entities;
using BookNest.Services.Interface;
using First.Data;
using Microsoft.EntityFrameworkCore;

namespace BookNest.Services
{
    public class PublisherService : IPublisherService
    {

        private readonly ApplicationDbContext _context;

        public PublisherService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddPublisher(InsertPublisherDto publisherDto)
        {
            try
            {
                if (publisherDto == null || string.IsNullOrWhiteSpace(publisherDto.Name) || string.IsNullOrWhiteSpace(publisherDto.Description))
                {
                    throw new ArgumentException("Invalid publisher data.");
                }

                var newPublisher = new Publisher
                {
                    PublisherId = Guid.NewGuid(),
                    Name = publisherDto.Name,
                    Description = publisherDto.Description
                };

                await _context.Publishers.AddAsync(newPublisher);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding publisher: {ex.Message}");
            }
        }


        public async Task DeletePublisher(Guid id)
        {
            var publisher = await _context.Publishers.FirstOrDefaultAsync(p => p.PublisherId == id);
            if (publisher == null)
                throw new KeyNotFoundException("Publisher not found.");

            _context.Publishers.Remove(publisher);
            await _context.SaveChangesAsync();
        }


        public async Task<List<GetAllPublisherDto>> GetAllPublishers()
        {
            try
            {
                var publishers = await _context.Publishers.ToListAsync();

                var publisherDtos = publishers.Select(p => new GetAllPublisherDto
                {
                    PublisherId = p.PublisherId,
                    Name = p.Name,
                    Description = p.Description
                }).ToList();

                return publisherDtos;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving publishers: {ex.Message}");
            }
        }


        public async Task<GetAllPublisherDto> GetPublisherById(Guid id)
        {
            var publisher = await _context.Publishers.FindAsync(id);

            if (publisher == null)
                throw new KeyNotFoundException("Publisher not found");

            return new GetAllPublisherDto
            {
                PublisherId = publisher.PublisherId,
                Name = publisher.Name,
                Description = publisher.Description
            };
        }

        public async Task UpdatePublisher(Guid id, UpdatePublisherDto publisherDto)
        {
            var publisher = await _context.Publishers.FindAsync(id);

            if (publisher == null)
                throw new Exception("Publisher not found");

            publisher.Name = publisherDto.Name;
            publisher.Description = publisherDto.Description;

            _context.Publishers.Update(publisher);
            await _context.SaveChangesAsync();
        }

    }
}
