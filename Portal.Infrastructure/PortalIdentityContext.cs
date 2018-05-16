using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Portal.core.Membership;

namespace Portal.Infrastructure
{
    public class PortalIdentityContext :IdentityDbContext<User>
    {
        public PortalIdentityContext(DbContextOptions<PortalIdentityContext> options):base(options)
        {
            
        }
    }
}
