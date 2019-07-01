using Abilities.Domain.Entities;
using Abilities.Persistence.EFInMemory.Configurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Abilities.Persistence.Configurations
{
    public class MysticalPowerConfiguration : TieredAbilityConfiguration<MysticalPower>
    {
        public override void Configure(EntityTypeBuilder<MysticalPower> builder)
        {
            base.Configure(builder);

            builder.Property(m => m.Material);
        }
    }
}
