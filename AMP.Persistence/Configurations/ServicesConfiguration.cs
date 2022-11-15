using AMP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AMP.Persistence.Configurations
{
    public sealed class ServicesConfiguration : DatabaseConfigurationBase<Services>
    {
        public override void Configure(EntityTypeBuilder<Services> builder)
        {
            base.Configure(builder);
            builder.HasIndex(x => x.Name)
                .HasDatabaseName("IX_Services_Name");
            builder.HasIndex(x => x.Description)
                .HasDatabaseName("IX_Services_Desc");
            builder.Property(a => a.Name)
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(40);
            builder.Property(a => a.Description)
                .HasColumnType("nvarchar")
                .HasMaxLength(150);
            
        }
    }
}