
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
    }
}
