using System;
using System.Collections.Generic;
using System.Text;

namespace NewsSite.Domain.Entities
{
    public class Like
    {
        public int LikeId { get; set; }

        public int CommentId { get; set; }
        public Comment Comment { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public bool Liked { get; set; }
    }
}
