
using NewsSite.Domain.Abstract;
using NewsSite.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewsSite.Domain.Concrete
{
    public class EFPostRepository: IPostRepository
    {
        // EFDbContext context = new EFDbContext();
        private EFDbContext _context;
        public EFPostRepository(EFDbContext context)
        {
            this._context = context;
        }

        public IEnumerable<Post> Posts
        {
            get { return _context.Posts; }
        }
    }
}
