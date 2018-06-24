using Microsoft.EntityFrameworkCore;
using Portal.core.Media;
using Portal.core.News;

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

        public DbSet<ContentPicture> ContentPictures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            modelBuilder.Entity<Content>().HasOne(x => x.Group);
            modelBuilder.Entity<ContentPicture>().HasOne(x => x.Picture).WithMany().HasForeignKey(x=>x.PictureId);
            modelBuilder.Entity<ContentPicture>().HasOne(x => x.Content).WithMany(x => x.ContentPictures).HasForeignKey(x=>x.ContentId);


        }
    }
}
