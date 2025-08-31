using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Feedback
    {
        public int Id { get; set; }
        public int TaskID { get; set; }
        public SubjectTask Task { get; set; } = null!;
        public int StudentID { get; set; }
        public Student Student { get; set; } = null!;
        public int AssistantID { get; set; }
        public Assistant Assistant { get; set; } = null!;
        public int Rating { get; set; }
        public string Comment { get; set; } = string.Empty;
        public string ReplyFromStudent { get; set; } = string.Empty;
    }
}
