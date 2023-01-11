using AMP.Domain.Entities.Base;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AMP.Persistence
{
    public abstract class DatabaseConfigurationBase<T> : IEntityTypeConfiguration<T> where T : EntityBase
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.ToTable(typeof(T).Name);
            builder.Ignore(x => x.IgnoreDateModified);
            builder.HasKey(a => a.Id)
                .IsClustered(false);
            builder.HasIndex(a => a.RowId)
                .IsUnique()
                .IsClustered();
            builder.Property(a => a.RowId)
                .UseIdentityColumn()
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            builder.Property(a => a.DateCreated)
                .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            builder.Property(a => a.DateModified).HasDefaultValue(DateTime.UtcNow);
            builder.Property(a => a.EntityStatus)
                .HasDefaultValue(EntityStatus.Normal)
                .HasConversion(new EnumToStringConverter<EntityStatus>())
                .HasColumnType("varchar")
                .HasMaxLength(36);
            builder.HasQueryFilter(a => a.EntityStatus == EntityStatus.Normal);
            builder.Property(a => a.Id)
                .HasColumnType("varchar")
                .HasMaxLength(36);
        }
    }
}
