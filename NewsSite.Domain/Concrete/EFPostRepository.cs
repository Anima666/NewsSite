
using Microsoft.EntityFrameworkCore;
using NewsSite.Domain.Abstract;
using NewsSite.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewsSite.Domain.Concrete
{
    public class EFPostRepository : IPostRepository
    {
        // EFDbContext context = new EFDbContext();
        private EFDbContext context;
        public EFPostRepository(EFDbContext context)
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

        public void SavePost(Post post, List<Tag> tags)
        {
            if (post.PostId == 0)
                context.Posts.Add(post);
            else
            {
                var posts = context.Posts.Include(e => e.PostTags).ThenInclude(e => e.Tag).Include(p => p.Category).ToList();
                // Post dbEntry = Posts.Find(post.PostId);  //mauby use using
                Post dbEntry = posts.Where(p=>p.PostId==post.PostId).FirstOrDefault();
                if (dbEntry != null)
                {
                    dbEntry.Title = post.Title;
                    dbEntry.Description = post.Description;
                    dbEntry.Text = post.Text;
                    dbEntry.DateChanged = DateTime.Now;

                    dbEntry.Category = post.Category; //неработает

                    var FirstTags = dbEntry.PostTags.Select(e => e.Tag);

                    //IEnumerable<Tag> Only = FirstTags.Except(tags);

                    if (FirstTags.Count() > 0)
                    {
                        dbEntry.PostTags.Clear();
                    }
                    //var newarr = context.Tags.Distinct();

                    for (int i = 0; i < tags.Count(); i++)
                    {
                        foreach (var item2 in context.Tags)
                        {
                            if (tags[i].Name != item2.Name)
                            {
                                // int index = tags.IndexOf();
                                //  tags.RemoveAt(i);
                                break;
                            }
                        }

                        context.Add(tags[i]);
                    }


                    foreach (var item in tags)
                    {
                        context.Add(new PostTag { Post = post, Tag = item });
                    }


                }
                context.SaveChanges();
            }
        }

        public void AddComment(int? parentId, int postId, string Text)
        {
            var Comment = new Comment()
            {
                ParentId = parentId,
                PostId = postId,
                Text = Text
            };
            context.Comments.Add(Comment);
            context.SaveChanges();
        }
    }
}
