using AMP.Domain.Entities;
using AMP.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AMP.Persistence.Configurations
{
    public class UsersConfiguration : DatabaseConfigurationBase<Users>
    {
        public override void Configure(EntityTypeBuilder<Users> builder)
        {
            base.Configure(builder);
            builder.Property(a => a.UserNo)
                .IsRequired();
            builder.Property(a => a.FirstName)
                .IsRequired();
            builder.Property(a => a.FamilyName)
                .IsRequired();
            builder.Property(a => a.IsSuspended)
                .HasDefaultValue(false);
            builder.Property(a => a.IsRemoved)
                .HasDefaultValue(false);
            builder.Property(a => a.Type)
                .HasDefaultValue(UserType.Customer)
                .HasConversion(new EnumToStringConverter<UserType>());
            builder.Property(a => a.LevelOfEducation)
                .HasConversion(new EnumToStringConverter<LevelOfEducation>());
            builder.OwnsOne(a => a.Contact, x =>
            {
                x.Property(a => a.PrimaryContact).IsRequired();
            });
            builder.OwnsOne(a => a.Address, x =>
            {
                x.Property(a => a.StreetAddress).IsRequired();
            });
        }
    }
}