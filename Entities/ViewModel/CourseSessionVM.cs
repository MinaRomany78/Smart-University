using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModel
{
    public class CourseSessionVM
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        public DateTime ReleaseDate { get; set; }
        public string MaterialLink { get; set; } = string.Empty;
        public int Order { get; set; }
        public int OptionalCourseId { get; set; }
    }
}
