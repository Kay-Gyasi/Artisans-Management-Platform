using AMP.Domain.Entities.BusinessManagement;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AMP.Persistence.Configurations.BusinessManagement
{
    public sealed class ServicesConfiguration : DatabaseConfiguration<Service>
    {
        public override void Configure(EntityTypeBuilder<Service> builder)
        {
            base.Configure(builder);
            builder.ToTable("Services");
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