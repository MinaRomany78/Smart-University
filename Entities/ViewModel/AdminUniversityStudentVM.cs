using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModel
{
    public class AdminUniversityStudentVM
    {
        public int? Id { get; set; } 
        [Required] 
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string NationalId { get; set; } = string.Empty;
        
        public bool IsEmailConfirmed { get; set; } = false;
        public int DepartmentId { get; set; }
        public int TermId { get; set; }

        public List<SelectListItem> DepartMentList { get; set; } = new();
        public List<SelectListItem> TermList { get; set; } = new();


    }
}
