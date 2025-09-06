using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum SubmissionStatus
{
    OnTime,
    Late,
    AcceptedLate
}

namespace Entities.Models
{
   
 
        public class TaskSubmission
        {
            public int Id { get; set; }

            public int TaskID { get; set; }
            public SubjectTask Task { get; set; } = null!;

            public int StudentID { get; set; }
            public Student Student { get; set; } = null!;
            public string SubmissionLink { get; set; }= string.Empty;
           public DateTime SubmissionDate { get; set; }

            public decimal? Grade { get; set; }

            public SubmissionStatus Status { get; set; } = SubmissionStatus.OnTime;
        public ICollection<Feedback> Feedbacks { get; set; }= new HashSet<Feedback>();
        }
    

}
