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


        public Task DeletePublisher(Guid id)
        {
            throw new NotImplementedException();
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


        public Task<GetAllPublisherDto> GetPublisherById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePublisher(Guid id, UpdatePublisherDto publisherDto)
        {
            throw new NotImplementedException();
        }
    }
}
