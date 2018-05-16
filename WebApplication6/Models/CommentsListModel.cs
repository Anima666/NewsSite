using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NewsSite.Domain.Entities;

namespace NewsSite.WebUi.Models
{
    public class CommentsListModel
    {
        public int? Seed { get; set; } //Корневой элемент
        public IEnumerable<Comment> Comments { get; set; }
    }
}
