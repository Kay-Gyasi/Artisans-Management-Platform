using AMP.Domain.Entities.UserManagement;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AMP.Persistence.Configurations.UserManagement
{
    public sealed class ImagesConfiguration : DatabaseConfigurationBase<Image>
    {
        public override void Configure(EntityTypeBuilder<Image> builder)
        {
            base.Configure(builder);
            builder.ToTable("Images");
            builder.Property(a => a.UserId)
                .HasColumnType("varchar")
                .HasMaxLength(36);
            builder.Property(a => a.PublicId)
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(30);
            builder.Property(a => a.ImageUrl)
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(150);
        }
    }
}