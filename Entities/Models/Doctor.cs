using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; } = null!;
        public ApplicationUser ApplicationUser { get; set; } = null!;
        public ICollection<SubjectTask> Tasks { get; } = new List<SubjectTask>();
        public ICollection<UniversityCourse> UniversityCourses { get; } = new List<UniversityCourse>();
        public ICollection<DoctorAssistant> DoctorAssistants { get; } = new List<DoctorAssistant>();
        public ICollection<Material> Materials { get; } = new List<Material>(); 
    }
}
