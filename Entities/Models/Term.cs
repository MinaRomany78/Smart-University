using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Term
    {
        public int Id { get; set; }
        public int Year { get; set; }          
        public int TermNumber { get; set; }    

        public string Name => $"Year {Year} - Term {TermNumber}";

        public ICollection<UniversityCourse> Courses { get; set; } = new List<UniversityCourse>();
        public ICollection<Student> Students { get; set; } = new List<Student>();
    }
}
