using BookNest.Dtos;

namespace BookNest.Services.Interface
{
    public interface IPublisherService
    {
        Task AddPublisher(InsertPublisherDto publisherDto);
        Task<List<GetAllPublisherDto>> GetAllPublishers();
        Task<GetAllPublisherDto> GetPublisherById(Guid id);
        Task UpdatePublisher(Guid id, UpdatePublisherDto publisherDto);
        Task DeletePublisher(Guid id);
    }
}
