using BookNest.Dtos;
using BookNest.Services.Interface;
using BookNest.Entities;
using BookNest.Data;
using First.Data;
using Microsoft.EntityFrameworkCore;

namespace BookNest.Services
{
    public class ReviewService : IReviewService
    {
        private readonly ApplicationDbContext _context; 

        public ReviewService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddReview(InsertReviewDto reviewDto)
        {
            var review = new Review
            {
                UserId = reviewDto.UserId,
                BookId = reviewDto.BookId,
                Rating = (int)Math.Round(reviewDto.Rating),
                Comment = reviewDto.Comment,
                CreatedAt = DateTime.UtcNow
            };

            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
        }


        public async Task<List<GetAllReviewDto>> GetReviewsForBook(Guid bookId)
        {
            var reviews = await _context.Reviews
                .Where(r => r.BookId == bookId)
                .Select(r => new GetAllReviewDto
                {
                    ReviewId = r.ReviewId,
                    BookId = r.BookId,
                    UserId = r.UserId,
                    Rating = r.Rating,
                    Comment = r.Comment,
                    CreatedAt = r.CreatedAt,
                    UserName = r.User.Name 
                })
                .ToListAsync();

            return reviews;
        }

    }
}
