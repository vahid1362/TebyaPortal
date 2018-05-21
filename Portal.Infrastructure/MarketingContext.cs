using Microsoft.EntityFrameworkCore;
using Portal.core.Media;
using QtasMarketing.Core.News;

namespace Portal.Infrastructure
{
  public  class PortalDbContext:DbContext
    {
        public PortalDbContext(DbContextOptions<PortalDbContext> options):base(options)
        {
            
        }

        
        public DbSet<Content> Contents { get; set; }

        public DbSet<Group> Groups { get; set; }

        public DbSet<Picture> Pictures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Content>().HasOne(x => x.Group);
        }
    }
}
