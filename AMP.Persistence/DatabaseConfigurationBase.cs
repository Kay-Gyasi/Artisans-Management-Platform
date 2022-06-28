using System;
using AMP.Domain.Entities.Base;
using AMP.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AMP.Persistence
{
    public abstract class DatabaseConfigurationBase<T> : IEntityTypeConfiguration<T> where T : EntityBase
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.ToTable(typeof(T).Name);
            builder.Property(a => a.DateModified).HasDefaultValue(DateTime.UtcNow);
            builder.HasQueryFilter(a => a.EntityStatus == EntityStatus.Normal);
        }
    }
}
