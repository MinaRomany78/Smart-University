using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    [PrimaryKey(nameof(DoctorId), nameof(AssistantId))]
    public class DoctorAssistant
    {
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; } = null!;
        public int AssistantId { get; set; }
        public Assistant Assistant { get; set; } = null!;
        
    }
}
