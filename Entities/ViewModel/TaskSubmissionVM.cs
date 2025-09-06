using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModel
{
  
        public class TaskSubmissionVM
        {
            public int SubmissionId { get; set; }
            public int TaskId { get; set; }
            public  int StudentId { get; set; }
            public string TaskTitle { get; set; } = string.Empty;
         
            public string StudentName { get; set; } = string.Empty;
            public DateTime SubmissionDate { get; set; }
            public string SubmissionLink { get; set; }= string.Empty;

           public decimal? Grade { get; set; }
            public SubmissionStatus Status { get; set; } = SubmissionStatus.OnTime; // إضافة الـ Enum
            public int CourseId { get; set; }



    }


}
