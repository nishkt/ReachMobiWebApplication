using Microsoft.EntityFrameworkCore;
using ReachMobiWebApplication.Models.Domain;

namespace ReachMobiWebApplication.Data
{
    public class ReachMobiDbContext : DbContext 
    {
        public ReachMobiDbContext(DbContextOptions options) : base(options)
        { 
        }

        public DbSet<ClickData> ClickData { get; set; }
    }
}
