using NewsSite.Domain.Entities;
using NewsSite.WebUi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsSite.WebUi.ViewModel
{
    public class PostSummaryViewModel
    {
        public Post Post { get; set; }
        public IEnumerable<Rating> Ratings { get; set; }
    }
}
