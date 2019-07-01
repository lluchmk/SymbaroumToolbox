using Abilities.Domain.Entities;
using Abilities.Persistence.EFInMemory.Configurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Abilities.Persistence.Configurations
{
    public class AbilityConfiguration : TieredAbilityConfiguration<Ability>
    {
        public override void Configure(EntityTypeBuilder<Ability> builder)
        {
            base.Configure(builder);
        }
    }
}
