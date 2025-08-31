using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class ApplicationUserOtp
    {
        public int Id { get; set; }
        [Required]
        public string OtpNumber { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public bool Status { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string ? Reason { get; set; }
        public string ApplicationUserId { get; set; } = string.Empty;
        public ApplicationUser ApplicationUser { get; set; } = null!;
    }
}
