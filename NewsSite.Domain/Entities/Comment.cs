using System;
using System.Collections.Generic;
using System.Text;

namespace NewsSite.Domain.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }

        public string Text { get; set; }

        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
