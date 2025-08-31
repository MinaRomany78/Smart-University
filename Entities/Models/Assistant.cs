using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Assistant
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; } = null!;
        public ApplicationUser ApplicationUser { get; set; } = null!;
        public ICollection<Feedback> Feedbacks { get; } = new List<Feedback>();
        public ICollection<Comment> Comments { get; } = new List<Comment>();
        public ICollection<DoctorAssistant> DoctorAssistants { get; } = new List<DoctorAssistant>();
        public ICollection<AssistantCourse> AssistantCourses { get; } = new List<AssistantCourse>();
    }
}
