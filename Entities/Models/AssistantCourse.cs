using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    [PrimaryKey(nameof(AssistantId), nameof(CourseId))]
    public class AssistantCourse
    {
        public int AssistantId { get; set; }
        public Assistant Assistant { get; set; } = null!;
        public int CourseId { get; set; }
        public UniversityCourse Course { get; set; } = null!;
    }
}
