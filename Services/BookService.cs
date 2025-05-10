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
                PublicationDate = bookDto.PublicationDate,
                Language = bookDto.Language,
                Description = bookDto.Description,
                AwardWinners = bookDto.AwardWinners
            };

            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
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
                .Select(book => new GetAllBookDto
                {
                    BookId = book.BookId,
                    Title = book.Title,
                    Price = book.Price
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
