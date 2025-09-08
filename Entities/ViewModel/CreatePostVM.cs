using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModel
{
    public class CreatePostVM
    {
        [Required(ErrorMessage = "Content is required.")]
        public string Content { get; set; } = string.Empty;
        public IFormFileCollection?Files { get; set; } 

        public string? Link { get; set; }
        public int CourseId { get; set; }
    }

}
