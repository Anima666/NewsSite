
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
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Category> Categories { get; set; }

        public EFDbContext(DbContextOptions<EFDbContext> options): base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PostTag>()
                .HasKey(t => new { t.PostId, t.TagId });
        }
    }
}
