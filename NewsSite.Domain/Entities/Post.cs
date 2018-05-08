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
        public string Category { get; set; }
       // public decimal Price { get; set; }
    }
}
