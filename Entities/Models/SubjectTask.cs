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
        public int? DoctorId { get; set; }
        public Doctor? Doctor { get; set; }

        public int? AssistantId { get; set; }
        public Assistant? Assistant { get; set; }

        public ICollection<Feedback> Feedbacks { get; set; }= new List<Feedback>();
        public ICollection<TaskSubmission> TaskSubmissions { get; set; }=new List<TaskSubmission>();

    }
}
