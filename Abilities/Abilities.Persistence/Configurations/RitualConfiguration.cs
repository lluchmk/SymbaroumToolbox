using Abilities.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Abilities.Persistence.Configurations
{
    public class RitualConfiguration : BaseConfiguration<Ritual>
    {
        public override void Configure(EntityTypeBuilder<Ritual> builder)
        {
            base.Configure(builder);

            builder.Property(r => r.Tradition).HasColumnName("Tradition");
        }
    }
}
