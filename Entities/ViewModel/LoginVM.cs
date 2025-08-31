using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModel
{
    public class LoginVM
    {
        [Required]

        public string UserNameOrEmail { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }=null!;

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
