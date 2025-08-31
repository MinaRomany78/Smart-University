using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModel
{
    public class ResetPasswordVM
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public string Otp { get; set; }
    }
}
