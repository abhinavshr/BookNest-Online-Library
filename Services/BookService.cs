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
                Description = bookDto.Description
            };

            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
        }


        public Task DeleteBook(Guid id)
        {
            throw new NotImplementedException();
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



        public Task<GetAllBookDto> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateBook(Guid id, UpdateBookDto bookDto)
        {
            throw new NotImplementedException();
        }
    }
}
