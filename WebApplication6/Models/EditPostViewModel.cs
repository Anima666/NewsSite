using NewsSite.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsSite.WebUi.Models
{
    public class EditPostViewModel
    {
        public Post Post { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
    }
}
