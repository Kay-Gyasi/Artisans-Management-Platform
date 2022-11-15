using AMP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AMP.Persistence.Configurations
{
    public sealed class LanguagesConfiguration : DatabaseConfigurationBase<Languages>
    {
        public override void Configure(EntityTypeBuilder<Languages> builder)
        {
            base.Configure(builder);
            builder.HasIndex(x => x.Name)
                .HasDatabaseName("Index_Lang_Name");
            builder.Property(a => a.Name)
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(30);
        }
    }
}