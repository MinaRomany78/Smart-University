using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModel
{
    public class NewCourseApplicationVM
    {
        [Required]
        public string CourseName { get; set; } = string.Empty;
        [Required]
        public string Experience { get; set; } = string.Empty;
    }
}
