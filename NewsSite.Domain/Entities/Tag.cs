using System;
using System.Collections.Generic;
using System.Text;

namespace NewsSite.Domain.Entities
{
    public class Tag
    {
        public int TagId { get; set; }
        public string Name { get; set; }

        //public virtual ICollection<Post> Posts { get;  } = new List<Post>();
        public virtual ICollection<PostTag> PostTags { get; } = new List<PostTag>();

        //public Tag()
        //{
        //    Posts = new List<Post>();
        //}
    }
}
