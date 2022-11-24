using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AMP.Persistence.Configurations
{
    public sealed class ImagesConfiguration : DatabaseConfigurationBase<Images>
    {
        public override void Configure(EntityTypeBuilder<Images> builder)
        {
            base.Configure(builder);
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