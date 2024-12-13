using Microsoft.EntityFrameworkCore;
using Sys.Domain.Entities.Sensor;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Sys.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sys.Presistence.DataContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
         : base(options)
        {
        }
        public DbSet<Sensor> Sensors { get; set; }
    }
}
