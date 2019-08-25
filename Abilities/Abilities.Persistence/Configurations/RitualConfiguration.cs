using Abilities.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Abilities.Persistence.Configurations
{
    public class RitualConfiguration : IEntityTypeConfiguration<Ritual>
    {
        public void Configure(EntityTypeBuilder<Ritual> builder)
        {
            builder.Property(r => r.Tradition).HasColumnName("Tradition");
        }
    }
}
