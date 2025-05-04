using BookNest.Entities;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace First.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<BookFormat> BookFormats { get; set; }
        public DbSet<Bookmark> Bookmarks { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<OrderHistory> OrderHistories { get; set; }
    }
}
