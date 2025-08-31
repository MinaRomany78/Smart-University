using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; } = null!;
        public ApplicationUser ApplicationUser { get; set; } = null!;
        public int OptionalCourseId { get; set; }
        public OptionalCourse OptionalCourse { get; set; } = null!;
        public decimal PricePaid { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? PromoCodeId { get; set; }
        public PromoCode? PromoCode { get; set; }
    }
}
