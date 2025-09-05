using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class SessionTask
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public string? SubmissionLink { get; set; }
        public DateTime? SubmittedAt { get; set; }
        public bool IsCompleted { get; set; } = false;
        public int CourseSessionId { get; set; }
        public CourseSession CourseSession { get; set; } = null!;
        public string ApplicationUserId { get; set; } = null!;
        public ApplicationUser ApplicationUser { get; set; } = null!;
    }
}
