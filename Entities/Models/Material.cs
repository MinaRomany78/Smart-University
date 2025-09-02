using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public enum MaterialType
    {
        Lecture = 1,   // محاضرة
        Lab = 2        // لاب
    }

    public class Material
    {
        public int Id { get; set; }
        public int UniversityCourseID { get; set; }
        public UniversityCourse UniversityCourse { get; set; } = null!;

        public string MaterialLink { get; set; } = string.Empty;
        public MaterialType MaterialType { get; set; }
        public DateTime UploadDate { get; set; }

      
        public int? DoctorId { get; set; }
        public Doctor? Doctor { get; set; }

        public int? AssistantId { get; set; }
        public Assistant? Assistant { get; set; }
    }


}
