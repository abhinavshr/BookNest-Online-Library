using BookNest.Dtos;

namespace BookNest.Services.Interface
{
    public interface IReviewService
    {
        Task AddReview(InsertReviewDto reviewDto);
        Task<List<GetAllReviewDto>> GetReviewsForBook(Guid bookId);
    }
}
