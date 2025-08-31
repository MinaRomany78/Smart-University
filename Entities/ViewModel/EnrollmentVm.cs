using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModel
{
    public class EnrollmentVM
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }=string.Empty;
        public int Credits { get; set; }
        public bool IsPaid { get; set; }
        public string DoctorName { get; set; } = string.Empty;

        public List<string> Assistants { get; set; } = new List<string>();
    }

}
