using Abilities.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Abilities.Persistence.EFInMemory.Configurations
{
    public class AbilityConfiguration : TieredAbilityConfiguration<Ability>
    {
        public override void Configure(EntityTypeBuilder<Ability> builder)
        {
            base.Configure(builder);
        }
    }
}
