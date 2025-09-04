using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.ViewModel
{
    public class MaterialVM
    {
        public int? Id { get; set; }
        public int UniversityCourseID { get; set; }   
        public string MaterialLink { get; set; } = string.Empty;
    }
}
