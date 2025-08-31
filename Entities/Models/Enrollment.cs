using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Enrollment
    {
        public int Id { get; set; }
        public int StudentID { get; set; }
        public Student Student { get; set; } = null!;
        public int UniversityCourseID { get; set; }
        public UniversityCourse UniversityCourse { get; set; } = null!;
        public DateTime EnrollmentDate { get; set; }
        public bool IsPaid { get; set; }
        public string Term { get; set; } = string.Empty;
        public int CreditHours { get; set; }
    }
}
