using NewsSite.Domain.Entities;
using NewsSite.WebUi.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsSite.WebUi.Models
{
    public class PostListViewModel
    {
        public IEnumerable<Post> Posts { get; set; }
        public IEnumerable<Rating> Ratings { get; set; }

        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
        public User User { get; set; }
    }
}
