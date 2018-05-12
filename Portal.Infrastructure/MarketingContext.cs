using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using QtasMarketing.Core.News;

namespace QtasMarketing.Infrastructure
{
  public  class MarketingDbContext:DbContext
    {
        public MarketingDbContext(DbContextOptions<MarketingDbContext> options):base(options)
        {
            
        }

        
        public DbSet<Content> Contents { get; set; }

        public DbSet<Group> Groups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Content>().HasOne(x => x.Group);
        }
    }
}
