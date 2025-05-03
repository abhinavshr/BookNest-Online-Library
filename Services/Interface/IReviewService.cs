using BookNest.Dtos;

namespace BookNest.Services.Interface
{
    public interface IReviewService
    {
        void AddReview(InsertReviewDto reviewDto);
        List<GetAllReviewDto> GetReviewsForBook(Guid bookId);
    }
}
