using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<UniversityCourse> Courses { get; set; } = new List<UniversityCourse>();
        public ICollection<Student> Students { get; set; } = new List<Student>();

    }
}
