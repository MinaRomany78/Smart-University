using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public enum TicketPriority
    {
        Low,
        Medium,
        High
    }
    public enum TicketStatus
    {
        Open,
        InProgress,
        Resolved,
        Closed
    }

    public class SupportTicket
    {
        public int Id { get; set; }
        [Required]
        public string IssueDescription { get; set; } = string.Empty;
        public TicketStatus Status { get; set; } 
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? ResolvedDate { get; set; }
        public TicketPriority Priority { get; set; }
        [Required]
        public string SenderName { get; set; } = string.Empty;
        [Required, EmailAddress]
        public string SenderEmail { get; set; } = string.Empty;
        public string? AdminResponse { get; set; }
    }
}
