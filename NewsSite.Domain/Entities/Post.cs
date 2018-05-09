using System;
using System.Collections.Generic;
using System.Text;

namespace NewsSite.Domain.Entities
{
    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public virtual ICollection<PostTag> PostTags { get; } = new List<PostTag>();

    }
}
