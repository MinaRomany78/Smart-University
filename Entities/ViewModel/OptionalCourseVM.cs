using Entities.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModel
{
    public class OptionalCourseVM
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public decimal Price { get; set; }
        public bool IsAvailableForUniversityStudents { get; set; }
        [Required]
        public int InstructorId { get; set; }
        public IFormFile? MainImg { get; set; }
        public List<Instructor> Instructors { get; set; } = new List<Instructor>();
    }
}
