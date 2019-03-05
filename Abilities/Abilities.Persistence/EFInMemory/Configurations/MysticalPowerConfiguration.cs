using Abilities.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Abilities.Persistence.EFInMemory.Configurations
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
