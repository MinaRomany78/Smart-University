using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int PostID { get; set; }
        public CommunityPost Post { get; set; } = null!;
        public string AuthorId { get; set; } = string.Empty;
        [ForeignKey(nameof(AuthorId))]
        public ApplicationUser Author { get; set; } = null!;
        public string Content { get; set; } = string.Empty;
        public DateTime DatePosted { get; set; }
    }
}
