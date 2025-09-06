using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModel
{
    public class TaskSubmissionCreateVM
    {
        public int TaskId { get; set; }
        public int CourseId { get; set; }
        public string Color { get; set; } = "#0d6efd";

        [Required]
        [Url(ErrorMessage = "Please enter a valid URL")]
        public string SubmissionLink { get; set; }=string.Empty;
    }

}
