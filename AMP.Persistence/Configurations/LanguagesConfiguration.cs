using AMP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AMP.Persistence.Configurations
{
    public sealed class LanguagesConfiguration : DatabaseConfigurationBase<Languages>
    {
        public override void Configure(EntityTypeBuilder<Languages> builder)
        {
            builder.Property(a => a.Name)
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(30);
            base.Configure(builder);
        }
    }
}