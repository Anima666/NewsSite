
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
            get { return context.Posts.Include(e => e.PostTags).ThenInclude(e => e.Tag).Include(c => c.Category).Include(u => u.User).ToList(); }
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
        public IEnumerable<Like> Likes
        {
            get { return context.Likes; }
        }
        public IEnumerable<Rating> Ratings
        {
            get { return context.Ratings ; }
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
            post.UserId = userId;
            post.Category = context.Categories.Where(c => c.Name == post.Category.Name).First();
            context.Posts.Add(post);

            AddTagsInDb(post, tags);
        }

        private void AddTagsInDb(Post post, List<Tag> tags)
        {
            foreach (var item in tags)
            {
                Tag removeTag = context.Tags.Where(c => c.Name == item.Name).FirstOrDefault();
                if (removeTag != null)
                    context.Tags.Remove(removeTag);
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
                UserId = UserId,
                PostedTime = DateTime.Now,
            };
            context.Comments.Add(Comment);
            context.SaveChanges();
        }

        public bool CheckUserPressRatings(User CurentUser, Post post)
        {
            Rating rating = context.Ratings.Where(x => x.UserId == CurentUser.Id & x.PostId == post.PostId).FirstOrDefault();

            if (rating == null)
            {
                return true;
            }

            return false;
        }

        public void SetRating(int id, Post post, User CurentUser, int value)
        {
            string currentUserId = CurentUser.Id;
            Rating rating = context.Ratings.Where(x => x.UserId == CurentUser.Id & x.PostId == post.PostId).FirstOrDefault();

            if (rating == null)
            {
                int counRatings = context.Ratings.Where(x => x.PostId == post.PostId).Count()+1;

                if (counRatings == 0)
                    counRatings = 1;
                    
                post.Rating = (post.Rating + value) / (counRatings);
                Rating newRating = new Rating{
                    PostId = post.PostId,
                    UserId =  currentUserId,
                };

                context.Add(newRating);
                context.SaveChanges();
            }
           
        }
        public void SetLike(int id, Comment comment, User CurentUser)
        {
            string currentUserId = CurentUser.Id;
            Like like = context.Likes.Where(x => x.UserId == CurentUser.Id & x.CommentId == comment.Id).FirstOrDefault();

            if (like == null)
            {
                comment.LikeCount++;

                User commentUser = context.Users.Where(u => u.Id == comment.UserId).FirstOrDefault();

                commentUser.Likes++;

                Like newLike = new Like();
                newLike.CommentId = id;
                newLike.UserId = currentUserId;

                context.Add(newLike);
                context.SaveChanges();
            }
        }
    }
}
