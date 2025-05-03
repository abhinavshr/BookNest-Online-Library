using BookNest.Dtos;
using BookNest.Entities;
using BookNest.Services.Interface;
using First.Data;

namespace BookNest.Services
{
    public class BookService : IBookService
    {
        private readonly ApplicationDbContext _context;

        public BookService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddBook(InsertBookDto bookDto)
        {
            try
            {
                var book = new Book
                {
                    Title = bookDto.Title,
                    ISBN = bookDto.ISBN,
                    AuthorId = bookDto.AuthorId,
                    PublisherId = bookDto.PublisherId,
                    GenreId = bookDto.GenreId,
                    Language = bookDto.Language,
                    FormatId = bookDto.FormatId,
                    Description = bookDto.Description,
                    Price = bookDto.Price,
                    Rating = bookDto.Rating,
                    Stock = bookDto.Stock,
                    PhysicalAvailability = bookDto.PhysicalAvailability,
                    PublicationDate = bookDto.PublicationDate,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _context.Books.Add(book);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding book: " + ex.Message);
            }
        }

        public void DeleteBook(Guid id)
        {
            try
            {
                var book = _context.Books.FirstOrDefault(b => b.BookId == id);
                if (book == null)
                    throw new Exception("Book Not Found");

                _context.Books.Remove(book);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting book: " + ex.Message);
            }
        }

        public List<GetAllBookDto> GetAllBooks()
        {
            try
            {
                var books = _context.Books.ToList();

                if (!books.Any())
                    throw new Exception("No books found");

                var result = books.Select(b => new GetAllBookDto
                {
                    BookId = b.BookId,
                    Title = b.Title,
                    ISBN = b.ISBN,
                    AuthorId = b.AuthorId,
                    PublisherId = b.PublisherId,
                    GenreId = b.GenreId,
                    Language = b.Language,
                    FormatId = b.FormatId,
                    Description = b.Description,
                    Price = b.Price,
                    Rating = b.Rating,
                    Stock = b.Stock,
                    PhysicalAvailability = b.PhysicalAvailability,
                    PublicationDate = b.PublicationDate
                }).ToList();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving books: " + ex.Message);
            }
        }

        public GetAllBookDto GetById(Guid id)
        {
            try
            {
                var book = _context.Books.FirstOrDefault(b => b.BookId == id);

                if (book == null)
                    throw new Exception("Book Not Found");

                var result = new GetAllBookDto
                {
                    BookId = book.BookId,
                    Title = book.Title,
                    ISBN = book.ISBN,
                    AuthorId = book.AuthorId,
                    PublisherId = book.PublisherId,
                    GenreId = book.GenreId,
                    Language = book.Language,
                    FormatId = book.FormatId,
                    Description = book.Description,
                    Price = book.Price,
                    Rating = book.Rating,
                    Stock = book.Stock,
                    PhysicalAvailability = book.PhysicalAvailability,
                    PublicationDate = book.PublicationDate
                };

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving book: " + ex.Message);
            }
        }

        public void UpdateBook(Guid id, UpdateBookDto bookDto)
        {
            try
            {
                var book = _context.Books.FirstOrDefault(b => b.BookId == id);

                if (book == null)
                    throw new Exception("Book Not Found");

                book.Title = bookDto.Title;
                book.ISBN = bookDto.ISBN;
                book.AuthorId = bookDto.AuthorId;
                book.PublisherId = bookDto.PublisherId;
                book.GenreId = bookDto.GenreId;
                book.Language = bookDto.Language;
                book.FormatId = bookDto.FormatId;
                book.Description = bookDto.Description;
                book.Price = bookDto.Price;
                book.Rating = bookDto.Rating;
                book.Stock = bookDto.Stock;
                book.PhysicalAvailability = bookDto.PhysicalAvailability;
                book.PublicationDate = bookDto.PublicationDate;
                book.UpdatedAt = DateTime.UtcNow;

                _context.Books.Update(book);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating book: " + ex.Message);
            }
        }
    }
}
