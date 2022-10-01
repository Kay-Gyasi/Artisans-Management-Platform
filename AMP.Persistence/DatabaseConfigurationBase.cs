using AMP.Domain.Entities.Base;
using AMP.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AMP.Persistence
{
    public abstract class DatabaseConfigurationBase<T> : IEntityTypeConfiguration<T> where T : EntityBase
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.ToTable(typeof(T).Name);
            //builder.Property(a => a.DateModified).HasDefaultValue(DateTime.UtcNow);
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
