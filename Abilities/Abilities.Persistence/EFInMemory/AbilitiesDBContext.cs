using Abilities.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Abilities.Persistence.EFInMemory
{
    public class AbilitiesDbContext : DbContext
    {
        public AbilitiesDbContext(DbContextOptions<AbilitiesDbContext> options)
            : base(options)
        {
        }

        public DbSet<Ability> Abilities { get; set; }

        public DbSet<MysticalPower> MysticalPowers { get; set; }

        public DbSet<Ritual> Rituals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AbilitiesDbContext).Assembly);
        }
    }
}
