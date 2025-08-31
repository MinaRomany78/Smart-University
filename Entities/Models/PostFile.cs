using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    
        public class PostFile
        {
            public int Id { get; set; }
            public int PostId { get; set; }
            public CommunityPost Post { get; set; } = null!;
            public string FilePath { get; set; } = string.Empty;
        }

      
}
