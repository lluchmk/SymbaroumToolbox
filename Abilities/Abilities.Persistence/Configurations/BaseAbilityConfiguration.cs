using Abilities.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Abilities.Persistence.Configurations
{
    public abstract class BaseConfiguration : IEntityTypeConfiguration<BaseAbility>
    {
        public void Configure(EntityTypeBuilder<BaseAbility> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd();

            builder.Property(a => a.UserId).HasColumnName("UserId");
            builder.Property(a => a.Name).HasColumnName("Name");
            builder.Property(a => a.Description).HasColumnName("Description");
        }
    }
}
