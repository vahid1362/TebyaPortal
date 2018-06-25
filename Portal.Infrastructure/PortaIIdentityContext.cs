using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Portal.core.Membership;

namespace Portal.Infrastructure
{
   public  class PortaIIdentityContext:IdentityDbContext<User>
    {
        public PortaIIdentityContext(DbContextOptions<PortaIIdentityContext> options ):base(options)
        {
            
        }
    }
}
