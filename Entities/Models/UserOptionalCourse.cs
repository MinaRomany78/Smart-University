using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    [PrimaryKey(nameof(ApplicationUserId), nameof(OptionalCourseId))]
    public class UserOptionalCourse
    {
        public string ApplicationUserId { get; set; } = null!;
        public ApplicationUser ApplicationUser { get; set; } = null!;
        public int OptionalCourseId { get; set; }
        public OptionalCourse OptionalCourse { get; set; } = null!;
        public DateTime PurchaseDate { get; set; }
        [ForeignKey(nameof(AppliedPromoCode))]
        public string? AppliedPromoCode { get; set; }
        public PromoCode? AppliedPromoCodeEntity { get; set; }
    }
}
