using NewsSite.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsSite.WebUi.Models
{
    public class CommentsModel
    {
        public CommentsModel(Comment comments)
        {
            this.Id = comments.Id;
            this.ParentId = comments.ParentId;
            this.Text = comments.Text;
        }
        public int Id { get; set; } 
        public int? ParentId { get; set; } 
        public string Text { get; set; } 
    }
}
