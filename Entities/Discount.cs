using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookNest.Entities
{
    public class Discount
    {
        [Key]
        public Guid DiscountId { get; set; }

        [ForeignKey(nameof(Book))] public Guid? BookId { get; set; } // Nullable

        public string DiscountType { get; set; } // Percentage, Fixed

        public decimal Value { get; set; }

        public bool IsOnSale { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public Discount()
        {
            DiscountId = Guid.NewGuid();
        }
    }

    
    }
