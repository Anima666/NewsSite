using System;
using System.Collections.Generic;
using System.Text;

namespace NewsSite.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Post> Posts { get; set; }
    }
}
