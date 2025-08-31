using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModel
{
    public class CartItemVM
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; } = string.Empty;
        public decimal OriginalPrice { get; set; }
        public decimal FinalPrice { get; set; }
        public string? AppliedPromoCode { get; set; }
        public decimal? DiscountPercentage { get; set; }
    }
    public class CartVM
    {
        public List<CartItemVM> Items { get; set; } = new();
        public decimal TotalPrice { get; set; }
    }
}
