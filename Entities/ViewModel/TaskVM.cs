using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModel
{
        public class TaskVM
        {
            public int Id { get; set; }

            [Required]
            public int UniversityCourseID { get; set; }

            [Required]
            [StringLength(100)]
            public string Title { get; set; } = string.Empty;

            [Required]
            [StringLength(500)]
            public string Description { get; set; } = string.Empty;

            [Required]
            public DateTime Deadline { get; set; }
        }
}


