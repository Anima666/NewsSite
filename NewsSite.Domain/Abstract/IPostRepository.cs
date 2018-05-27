using System;
using System.Collections.Generic;
using System.Text;
using NewsSite.Domain.Entities;

namespace NewsSite.Domain.Abstract
{
    public interface IPostRepository
    {
        IEnumerable<Post> Posts { get; }
        IEnumerable<Tag> Tags { get; }
        IEnumerable<Category> Categories { get; }

        IEnumerable<Comment> Comments { get; }
        IEnumerable<Rating> Ratings { get; }
        IEnumerable<User> Users { get; }

        IEnumerable<Like> Likes { get; }

        Post DeletePost(int postId);
        void SetLike(int id, Comment comment, User CurentUser);
        void SavePost(Post post, List<Tag> tags, string userId);
        void AddComment(int? parentId, int postId, string UserId, string Text);
        void SetRating(int id, Post post, User CurentUser,int value);
     
    }
}
