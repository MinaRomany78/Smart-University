using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModel
{
    public class CreatePostVM
    {
        public string Content { get; set; } = string.Empty;
        public IFormFileCollection?Files { get; set; } 

        public string? Link { get; set; }
        public int CourseId { get; set; }
    }

}
