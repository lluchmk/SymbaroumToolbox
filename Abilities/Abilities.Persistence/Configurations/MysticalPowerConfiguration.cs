using Abilities.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Abilities.Persistence.Configurations
{
    public class MysticalPowerConfiguration : IEntityTypeConfiguration<MysticalPower>
    {
        public void Configure(EntityTypeBuilder<MysticalPower> builder)
        {
            builder.Property(m => m.Material);
        }
    }
}
