using Abilities.Domain.Entities;
using Abilities.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Abilities.Persistence.EFInMemory.Configurations
{
    public class TieredAbilityConfiguration<TAbility> : BaseConfiguration<TAbility>
        where TAbility : TieredAbility
    {
        public override void Configure(EntityTypeBuilder<TAbility> builder)
        {
            base.Configure(builder);

            builder.OwnsOne(a => a.Novice).Property(t => t.Description).HasColumnName("NoviceDescription");
            builder.OwnsOne(a => a.Novice).Property(t => t.Type).HasColumnName("NoviceType");

            builder.OwnsOne(a => a.Adept).Property(t => t.Description).HasColumnName("AdeptDescription");
            builder.OwnsOne(a => a.Adept).Property(t => t.Type).HasColumnName("AdeptType");

            builder.OwnsOne(a => a.Master).Property(t => t.Description).HasColumnName("MasterDescription");
            builder.OwnsOne(a => a.Master).Property(t => t.Type).HasColumnName("MasterType");
        }
    }
}
