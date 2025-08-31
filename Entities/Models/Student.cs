using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; } = null!;
        public ApplicationUser ApplicationUser { get; set; } = null!;
        public string NationalID { get; set; } = null!;
        public bool IsUniversityStudent { get; set; }
        public string? PromoCode { get; set; } // FK to PromoCode
        public PromoCode? PromoCodeEntity { get; set; }
        public int TermId { get; set; }
        public Term Term { get; set; } = null!;
        public int DepartmentID { get; set; }
        public Department Department { get; set; } = null!;
        public ICollection<Enrollment> Enrollments { get; } = new List<Enrollment>();
        public ICollection<TaskSubmission> TaskSubmissions { get; } = new List<TaskSubmission>();
        public ICollection<Feedback> Feedbacks { get; } = new List<Feedback>();
        public ICollection<CommunityPost> CommunityPosts { get; } = new List<CommunityPost>();
        public ICollection<Comment> Comments { get; } = new List<Comment>();
    }
}
