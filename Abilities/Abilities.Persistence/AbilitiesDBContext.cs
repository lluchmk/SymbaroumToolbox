using Abilities.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Abilities.Persistence
{
    public class AbilitiesDbContext : DbContext
    {
        public AbilitiesDbContext(DbContextOptions<AbilitiesDbContext> options)
            : base(options)
        {
        }

        public DbSet<BaseAbility> Abilities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AbilitiesDbContext).Assembly);
        }
    }
}
