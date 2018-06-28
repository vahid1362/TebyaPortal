using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Portal.core.Membership;

namespace Portal.Infrastructure
{
    public  class PortaIIdentityContext:IdentityDbContext<AppUser>
    {

        public DbSet<AppUser> AppUsers { get; set; }

        public PortaIIdentityContext(DbContextOptions<PortaIIdentityContext> options ):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            var passwordHasher=new PasswordHasher<AppUser>();

            var user = new AppUser()
            {
                UserName = "Admin",

            };
            passwordHasher.HashPassword(user, "12346");
            builder.Entity<AppUser>().HasData(user);

            base.OnModelCreating(builder);
        }
    }
}
