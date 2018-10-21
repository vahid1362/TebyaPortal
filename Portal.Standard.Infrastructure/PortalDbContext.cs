using Microsoft.EntityFrameworkCore;
using Portal.core.News;
using Portal.Standard.Core.Media;
using Portal.Standard.Core.News;

namespace Portal.Standard.Infrastructure
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
            modelBuilder.Entity<Group>().HasData(new Group()
            {
                Id = 1,
                Title = "ورزشی",
                Description = "",
                Priority = 0,
                IsPrivate = false,
                DisplayInMain = true
            }, new Group()
            {
                Id=2,
                Title = "فن آوری اطلاعات",
                Description = "",
                Priority = 0,
                IsPrivate = false,
                DisplayInMain = true
            });
            modelBuilder.Entity<Content>().HasOne(x => x.Group);
            modelBuilder.Entity<ContentPicture>().HasOne(x => x.Picture).WithMany().HasForeignKey(x=>x.PictureId);
            modelBuilder.Entity<ContentPicture>().HasOne(x => x.Content).WithMany(x => x.ContentPictures).HasForeignKey(x=>x.ContentId);


        }
    }
}
