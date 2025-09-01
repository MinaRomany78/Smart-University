using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class CommunityPost
    {
        public int Id { get; set; }
        public int CourseID { get; set; }
        [ForeignKey(nameof(CourseID))]
        public UniversityCourse UniversityCourse { get; set; } = null!;
        public string AuthorId { get; set; } = string.Empty;
        [ForeignKey(nameof(AuthorId))]
        public ApplicationUser Author { get; set; } = null!;
        
        public string Content { get; set; } = string.Empty;
        public DateTime PostDate { get; set; }
        public ICollection<Comment> Comments { get; } = new List<Comment>();
        public ICollection<PostFile> Files { get; set; }= new List<PostFile>();
        public ICollection<PostLink> Links { get; set; }=new List<PostLink>();
    }
}
