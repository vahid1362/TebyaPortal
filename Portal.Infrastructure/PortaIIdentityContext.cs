using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Portal.core.Membership;

namespace Portal.Infrastructure
{
    public  class PortaIIdentityContext:IdentityDbContext<AppUser>
    {
        public PortaIIdentityContext(DbContextOptions<PortaIIdentityContext> options ):base(options)
        {
            
        }
    }
}
