using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModel
{
    public class AdminCourseVM
    {
        public int Id { get; set; }

        public string Name { get; set; }=string.Empty;

        public int CreditHours { get; set; }


     
        public int DepartmentId { get; set; }
        public int TermId { get; set; }

        public List<SelectListItem> DepartMentList { get; set; } = new();
        public List<SelectListItem> TermList { get; set; } = new();


    }
}
