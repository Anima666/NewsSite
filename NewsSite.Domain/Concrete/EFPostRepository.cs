
using Microsoft.EntityFrameworkCore;
using NewsSite.Domain.Abstract;
using NewsSite.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using System.Text;

namespace NewsSite.Domain.Concrete
{
    public class EFPostRepository : IPostRepository
    {
  
        private ApplicationContext context;
        public EFPostRepository(ApplicationContext context)
        {
            this.context = context;
        }

        public IEnumerable<Post> Posts
        {
            get { return context.Posts.Include(e => e.PostTags).ThenInclude(e => e.Tag).Include(c => c.Category).ToList(); }
        }

        public IEnumerable<Tag> Tags
        {
            get { return context.Tags; }
        }

        public IEnumerable<Category> Categories
        {
            get { return context.Categories; }
        }

        public IEnumerable<User> Users
        {
            get { return context.Users; }
        }

        public IEnumerable<Comment> Comments
        {
            get { return context.Comments.Include(p => p.Post).ToList(); }
        }


        public Post DeletePost(int postId)
        {
            Post dbEntry = context.Posts.Find(postId);
            if (dbEntry != null)
            {
                context.Posts.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public void SavePost(Post post, List<Tag> tags, string userId)
        {
            if (post.PostId == 0)
            {
                AddNewPost(post, tags, userId);
            }
            else
            {
                SaveCurrentPost(post, tags);
            }
            context.SaveChanges();
        }

        private void SaveCurrentPost(Post post, List<Tag> tags)
        {
            var allPost = context.Posts.Include(e => e.PostTags).ThenInclude(e => e.Tag).Include(p => p.Category).ToList();

            Post dbEntry = allPost.Where(p => p.PostId == post.PostId).First();
            if (dbEntry != null)
            {
                dbEntry.Title = post.Title;
                dbEntry.Description = post.Description;
                dbEntry.Text = post.Text;
                dbEntry.DateChanged = DateTime.Now;

                if (post.Path != null)
                    dbEntry.Path = post.Path;

                dbEntry.Category = context.Categories.Where(c => c.Name == post.Category.Name).First();

                var firstTags = dbEntry.PostTags.Select(e => e.Tag);

                if (firstTags.Count() > 0)
                    dbEntry.PostTags.Clear();

                AddTagsInDb(post, tags);
            }
        }

        private void AddNewPost(Post post, List<Tag> tags, string userId)
        {
            post.DateChanged = DateTime.Now;
            post.UserID = userId;

            context.Posts.Add(post);

            AddTagsInDb(post, tags);
        }

        private void AddTagsInDb(Post post, List<Tag> tags)
        {
            foreach (var item in tags)
            {
                context.Tags.RemoveRange(this.Tags.Where(c => c.Name == item.Name));
                context.Add(new PostTag { Post = post, Tag = item });
            }
        }

        public void AddComment(int? parentId, int postId, string UserId, string Text)
        {
            var Comment = new Comment()
            {
                ParentId = parentId,
                PostId = postId,
                Text = Text,
                UserID = UserId,
                PostedTime = DateTime.Now,
            };
            context.Comments.Add(Comment);
            context.SaveChanges();
        }
    }
}
