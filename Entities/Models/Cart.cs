using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    [PrimaryKey(nameof(ApplicationUserId), nameof(OptionalCourseId))]
    public class Cart
    {
        public ApplicationUser ApplicationUser { get; set; } = null!;
        public string ApplicationUserId { get; set; } = null!;
        public int OptionalCourseId { get; set; }
        public OptionalCourse OptionalCourse { get; set; } = null!;
        public string? AppliedPromoCode { get; set; }
        public decimal? DiscountPercentage { get; set; }
    }
}
