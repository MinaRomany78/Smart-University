using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModel
{
    public class CommunityVM
    {
        public UniversityCourse Course { get; set; } = null!;
        public List<CommunityPost> Posts { get; set; } = new();
        public string Color {  get; set; }=null!;
        public List<string> Assistants { get; set; } =new();

    }
}
