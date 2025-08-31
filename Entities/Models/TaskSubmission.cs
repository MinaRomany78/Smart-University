using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class TaskSubmission
    {
        public int Id { get; set; }
        public int TaskID { get; set; }
        public SubjectTask Task { get; set; } = null!;
        public int StudentID { get; set; }
        public Student Student { get; set; } = null!;
        public DateTime SubmissionDate { get; set; }
        public decimal? Grade { get; set; }
    }
}
