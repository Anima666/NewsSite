using NewsSite.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsSite.WebUi.Models
{
    public class PostListViewModel
    {
        public IEnumerable<Post> Posts { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentTag { get; set; }


        //тест
       // public IEnumerable<Comment> Comments { get; set; }

    }
}
