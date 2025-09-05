using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class CourseSession
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty; 
        public DateTime ReleaseDate { get; set; } 
        public string MaterialLink { get; set; } = string.Empty; 
        public int Order { get; set; }
        public bool IsLocked { get; set; } = true;
        public int OptionalCourseId { get; set; }
        public OptionalCourse OptionalCourse { get; set; } = null!;
        public ICollection<SessionTask> Tasks { get; set; } = new List<SessionTask>();
    }
}
