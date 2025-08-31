using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Material
    {
        public int Id { get; set; }
        public int UniversityCourseID { get; set; }
        public UniversityCourse UniversityCourse { get; set; } = null!;
        public string MaterialLink { get; set; } = string.Empty;
        public string MaterialType { get; set; } = string.Empty;
        public DateTime UploadDate { get; set; }
    }
}
