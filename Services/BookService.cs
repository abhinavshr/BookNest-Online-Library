using BookNest.Dtos;
using BookNest.Entities;
using BookNest.Services.Interface;
using First.Data;
using Microsoft.EntityFrameworkCore;

namespace BookNest.Services
{
    public class BookService : IBookService
    {
        private readonly IAuthorService _authorRepository;
        private readonly IGenreService _genreRepository;
        private readonly IPublisherService _publisherRepository;
        private readonly ApplicationDbContext _context;

        public BookService(ApplicationDbContext context, IAuthorService authorRepository, IGenreService genreRepository, IPublisherService publisherRepository)
        {
            _context = context;
            _authorRepository = authorRepository;
            _genreRepository = genreRepository;
            _publisherRepository = publisherRepository;
        }

        public async Task AddBook(InsertBookDto bookDto)
        {
            if (bookDto == null)
            {
                throw new ArgumentNullException(nameof(bookDto), "Book data cannot be null.");
            }

            var author = await _authorRepository.GetById(bookDto.AuthorId);
            if (author == null)
            {
                throw new ArgumentException("Invalid Author ID.");
            }

            var genre = await _genreRepository.GetById(bookDto.GenreId);
            if (genre == null)
            {
                throw new ArgumentException("Invalid Genre ID.");
            }

            var publisher = await _publisherRepository.GetPublisherById(bookDto.PublisherId);
            if (publisher == null)
            {
                throw new ArgumentException("Invalid Publisher ID.");
            }

            if (bookDto.PublicationDate == default(DateTime) || bookDto.PublicationDate == DateTime.MinValue)
            {
                bookDto.PublicationDate = DateTime.Now;
            }

            if (string.IsNullOrWhiteSpace(bookDto.Title))
            {
                throw new ArgumentException("Title cannot be empty.");
            }
            if (bookDto.Price <= 0)
            {
                throw new ArgumentException("Price must be greater than zero.");
            }

            var book = new Book
            {
                Title = bookDto.Title,
                ISBN = bookDto.ISBN,
                AuthorId = bookDto.AuthorId,
                GenreId = bookDto.GenreId,
                PublisherId = bookDto.PublisherId,
                Price = bookDto.Price,
                Stock = bookDto.Stock,
                PhysicalAvailability = bookDto.PhysicalAvailability,
                PublicationDate = bookDto.PublicationDate.ToUniversalTime(),
                Language = bookDto.Language,
                Description = bookDto.Description,
                AwardWinners = bookDto.AwardWinners
            };

            try
            {
                Console.WriteLine($"Publication Date: {bookDto.PublicationDate}");
                await _context.Books.AddAsync(book);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while adding the book.", ex);
            }
        }



        public async Task DeleteBook(Guid id)
        {
            var book = await _context.Books.FirstOrDefaultAsync(b => b.BookId == id);
            if (book == null)
                throw new KeyNotFoundException("Book not found.");

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }


        public async Task<List<GetAllBookDto>> GetAllBooks()
        {
            return await _context.Books
                .Include(book => book.Author)
                .Select(book => new GetAllBookDto
                {
                    BookId = book.BookId,
                    Title = book.Title,
                    Price = book.Price,
                    AuthorName = book.Author.Name
                })
                .ToListAsync();
        }

        public async Task<GetAllBookDto> GetTopSellingBook()
        {
            var topSelling = await _context.OrderItems
                .GroupBy(oi => oi.BookId)
                .Select(g => new
                {
                    BookId = g.Key,
                    SoldCount = g.Sum(oi => oi.Quantity)
                })
                .OrderByDescending(g => g.SoldCount)
                .FirstOrDefaultAsync();

            if (topSelling == null)
                return null;

            var book = await _context.Books
                .Include(b => b.Author)
                .Where(b => b.BookId == topSelling.BookId)
                .Select(book => new GetAllBookDto
                {
                    BookId = book.BookId,
                    Title = book.Title,
                    Price = book.Price,
                    AuthorName = book.Author.Name,
                    SoldCount = topSelling.SoldCount
                })
                .FirstOrDefaultAsync();

            return book;
        }

        public async Task<List<GetAllBookDto>> GetBooksWithAwards()
        {
            var awardWinningBooks = await _context.Books
                .Include(book => book.Author)
                .Where(book => !string.IsNullOrEmpty(book.AwardWinners))
                .Select(book => new GetAllBookDto
                {
                    BookId = book.BookId,
                    Title = book.Title,
                    Price = book.Price,
                    AuthorName = book.Author.Name,
                    AwardWinners = book.AwardWinners
                })
                .ToListAsync();

            if (awardWinningBooks.Count == 0)
            {
                return new List<GetAllBookDto>();
            }

            return awardWinningBooks;
        }

        public async Task<List<GetAllBookDto>> GetBooksPublishedInLastMonth()
        {
            var oneMonthAgo = DateTime.UtcNow.AddMonths(-1);

            return await _context.Books
                .Include(book => book.Author)
                .Where(book => book.PublicationDate >= oneMonthAgo)
                .Select(book => new GetAllBookDto
                {
                    BookId = book.BookId,
                    Title = book.Title,
                    Price = book.Price,
                    AuthorName = book.Author.Name
                })
                .ToListAsync();
        }


        public async Task<List<GetAllBookDto>> GetBooksPublishedInLastWeek()
        {
            var lastWeek = DateTime.UtcNow.AddDays(-7);

            return await _context.Books
                .Include(book => book.Author)
                .Where(book => book.PublicationDate >= lastWeek)
                .Select(book => new GetAllBookDto
                {
                    BookId = book.BookId,
                    Title = book.Title,
                    Price = book.Price,
                    AuthorName = book.Author.Name
                })
                .ToListAsync();
        }

        public async Task<List<GetAllBookDto>> GetComingSoonBooks()
        {
            var currentDate = DateTime.UtcNow;

            return await _context.Books
                .Include(book => book.Author)
                .Where(book => book.PublicationDate > currentDate)
                .Select(book => new GetAllBookDto
                {
                    BookId = book.BookId,
                    Title = book.Title,
                    Price = book.Price,
                    AuthorName = book.Author.Name
                })
                .ToListAsync();
        }


        public async Task<GetAllBookDto> GetById(Guid id)
        {
            var book = await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .Include(b => b.Genre)
                .FirstOrDefaultAsync(b => b.BookId == id);

            if (book == null)
            {
                throw new KeyNotFoundException("Book not found.");
            }

            var bookDto = new GetAllBookDto
            {
                BookId = book.BookId,
                Title = book.Title,
                ISBN = book.ISBN,
                AuthorId = book.AuthorId,
                PublisherId = book.PublisherId,
                GenreId = book.GenreId,
                Language = book.Language,
                Description = book.Description,
                Price = book.Price,
                Rating = book.Rating,
                Stock = book.Stock,
                PhysicalAvailability = book.PhysicalAvailability,
                PublicationDate = book.PublicationDate,
                AwardWinners = book.AwardWinners
            };

            return bookDto;
        }



        public async Task UpdateBook(Guid id, UpdateBookDto bookDto)
        {
            var book = await _context.Books
                .FirstOrDefaultAsync(b => b.BookId == id);

            if (book == null)
            {
                throw new KeyNotFoundException("Book not found.");
            }

            book.Title = bookDto.Title;
            book.ISBN = bookDto.ISBN;
            book.AuthorId = bookDto.AuthorId;
            book.PublisherId = bookDto.PublisherId;
            book.GenreId = bookDto.GenreId;
            book.Language = bookDto.Language;
            book.Description = bookDto.Description;
            book.Price = bookDto.Price;
            book.Rating = bookDto.Rating;
            book.Stock = bookDto.Stock;
            book.PhysicalAvailability = bookDto.PhysicalAvailability;
            book.PublicationDate = bookDto.PublicationDate;
            book.AwardWinners = bookDto.AwardWinners;

            await _context.SaveChangesAsync();
        }

    }
}
