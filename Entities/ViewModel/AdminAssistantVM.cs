using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModel
{
    public class AdminAssistantVM
    {
        public int? Id { get; set; }
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        public string FullName { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public bool IsEmailConfirmed { get; set; } = false;
        public string? Address { get; set; }
        [Required]
        public string UserName { get; set; } = string.Empty;
      
        public List<int> SelectedCourseIds { get; set; } = new();
        public List<SelectListItem> CoursesList { get; set; } = new();


    }
}
