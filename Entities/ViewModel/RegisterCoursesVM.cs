using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModel
{
    public class RegisterCoursesVM
    {
        public int CourseId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Credits { get; set; }
        public bool IsSelected { get; set; } 
        public bool IsPaid { get; set; }
        public string DoctorName { get; set; } = string.Empty;

        // public List<int> SelectedCourses{ get; set; }=new List<int>();
    }
}
