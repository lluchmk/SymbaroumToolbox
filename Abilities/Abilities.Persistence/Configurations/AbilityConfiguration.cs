using Abilities.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Abilities.Persistence.Configurations
{
    public class AbilityConfiguration : IEntityTypeConfiguration<Ability>
    {
        public void Configure(EntityTypeBuilder<Ability> builder)
        {
            builder.OwnsOne(a => a.Novice).Property(t => t.Description).HasColumnName("NoviceDescription");
            builder.OwnsOne(a => a.Novice).Property(t => t.Type).HasColumnName("NoviceType");

            builder.OwnsOne(a => a.Adept).Property(t => t.Description).HasColumnName("AdeptDescription");
            builder.OwnsOne(a => a.Adept).Property(t => t.Type).HasColumnName("AdeptType");

            builder.OwnsOne(a => a.Master).Property(t => t.Description).HasColumnName("MasterDescription");
            builder.OwnsOne(a => a.Master).Property(t => t.Type).HasColumnName("MasterType");
        }
    }
}
