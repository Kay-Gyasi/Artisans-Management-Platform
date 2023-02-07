using AMP.Domain.Entities.UserManagement;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AMP.Persistence.Configurations.UserManagement
{
    public sealed class LanguagesConfiguration : DatabaseConfiguration<Language>
    {
        public override void Configure(EntityTypeBuilder<Language> builder)
        {
            base.Configure(builder);
            builder.ToTable("Languages");
            builder.HasIndex(x => x.Name)
                .HasDatabaseName("Index_Lang_Name");
            builder.Property(a => a.Name)
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(30);
        }
    }
}