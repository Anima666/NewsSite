using Microsoft.EntityFrameworkCore;
using NewsSite.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewsSite.Domain.Concrete
{
    public class EFDbContext: DbContext
    {
        public DbSet<Post> Posts { get; set; }

        public EFDbContext(DbContextOptions<EFDbContext> options): base(options)
        {
        }
    }
}
