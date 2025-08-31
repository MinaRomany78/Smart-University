using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class OptionalCourse
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public string MainImg { get; set; } = string.Empty;
        [Required]
        public decimal Price { get; set; }
        public int Traffic { get; set; } = 0;
        public bool IsAvailableForUniversityStudents { get; set; }
        public string? PromoCode { get; set; }
        public PromoCode? PromoCodeEntity { get; set; }
        public int InstructorId { get; set; }
        public Instructor Instructor { get; set; } = null!;
        public ICollection<CourseReview> Reviews { get; set; } = new List<CourseReview>();
    }
}
