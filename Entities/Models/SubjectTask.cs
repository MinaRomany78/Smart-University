using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class SubjectTask
    {
        public int Id { get; set; }
        public int UniversityCourseID { get; set; }
        public UniversityCourse UniversityCourse { get; set; } = null!;
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime Deadline { get; set; }
        public int AssignedBy { get; set; } 
        public Doctor Doctor { get; set; } = null!;
        public ICollection<Feedback> Feedbacks { get; set; }= new List<Feedback>();

    }
}
