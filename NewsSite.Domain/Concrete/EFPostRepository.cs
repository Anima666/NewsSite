
using Microsoft.EntityFrameworkCore;
using NewsSite.Domain.Abstract;
using NewsSite.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewsSite.Domain.Concrete
{
    public class EFPostRepository: IPostRepository
    {
        // EFDbContext context = new EFDbContext();
        private EFDbContext context;
        public EFPostRepository(EFDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Post> Posts
        {
            get { return context.Posts.Include(e => e.PostTags).ThenInclude(e => e.Tag).ToList(); }
        }
        public IEnumerable<Tag> Tags
        {
            get { return context.Tags; }
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

        public void SavePost(Post post)
        {
            if (post.PostId == 0)
                context.Posts.Add(post);
            else
            {
                Post dbEntry = context.Posts.Find(post.PostId); //mauby use using
                if (dbEntry != null)
                {
                    dbEntry.Title = post.Title;
                    dbEntry.Description = post.Description;
                }
            }
            context.SaveChanges();
        }
    }
}
