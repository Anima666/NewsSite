using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NewsSite.Domain.Entities;
using NewsSite.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NewsSite.Domain.Concrete
{
    public class LocalizationContext: IdentityDbContext
    {
        public LocalizationContext(DbContextOptions<LocalizationContext> options)
            : base(options)
        { }
        public DbSet<Culture> Cultures { get; set; }
        public DbSet<Resource> Resources { get; set; }
    }
}
