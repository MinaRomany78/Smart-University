using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Application
    {
        public int Id { get; set; }
        [Required]
        public string NationalID { get; set; } = null!;
        [Required]
        public string Name { get; set; } = string.Empty;
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public int Age { get; set; }
        [Required]
        public string Gender { get; set; } = null!;
        [Required]
        public string Address { get; set; } = null!;
        [Required]
        public bool GenerateEmail { get; set; } = false;
        public string? GeneratedPassword { get; set; }
    }
}
