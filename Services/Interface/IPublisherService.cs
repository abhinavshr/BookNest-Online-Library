using BookNest.Dtos;

namespace BookNest.Services.Interface
{
    public interface IPublisherService
    {
        void AddPublisher(InsertPublisherDto publisherDto);
        List<GetAllPublisherDto> GetAllPublishers();
        GetAllPublisherDto GetPublisherById(Guid id);
        void UpdatePublisher(Guid id, UpdatePublisherDto publisherDto);
        void DeletePublisher(Guid id);
    }
}
