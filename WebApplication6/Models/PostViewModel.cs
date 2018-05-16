using NewsSite.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsSite.WebUi.Models
{
    public class PostViewModel
    {
        public Post Post { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
    }
}
