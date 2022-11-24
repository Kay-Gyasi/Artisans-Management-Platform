using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AMP.Persistence.Configurations
{
    public sealed class UsersConfiguration : DatabaseConfigurationBase<Users>
    {
        public override void Configure(EntityTypeBuilder<Users> builder)
        {
            base.Configure(builder);

            // TODO:: Add these indexes manually
            //builder.HasIndex(x => x.Contact.PrimaryContact)
                //.HasDatabaseName("IX_Contact");
            //builder.HasIndex(x => new {x.Contact.PrimaryContact, x.IsVerified})
                //.HasDatabaseName("IX_Search");
            builder.HasIndex(x => x.Type)
                .HasDatabaseName("IX_Type");
            builder.Property(a => a.FirstName)
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(50);
            builder.Property(a => a.FamilyName)
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(50);
            builder.Property(a => a.OtherName)
                .HasColumnType("varchar")
                .HasMaxLength(50);
            builder.Property(a => a.ImageId)
                .HasColumnType("varchar")
                .HasMaxLength(36);  
            builder.Property(a => a.DisplayName)
                .HasColumnType("varchar")
                .HasMaxLength(100);
            builder.Property(a => a.MomoNumber)
                .HasColumnType("varchar")
                .HasMaxLength(15);  
            builder.Property(a => a.IsSuspended)
                .HasDefaultValue(false);
            builder.Property(a => a.IsRemoved)
                .HasDefaultValue(false);
            builder.Property(a => a.IsVerified)
                .HasDefaultValue(false);
            builder.Property(a => a.Type)
                .HasDefaultValue(UserType.Customer)
                .HasConversion(new EnumToStringConverter<UserType>());
            builder.Property(a => a.LevelOfEducation)
                .HasConversion(new EnumToStringConverter<LevelOfEducation>());
            builder.OwnsOne(a => a.Contact, x =>
            {
                x.Property(a => a.PrimaryContact)
                    .IsRequired()
                    .HasColumnType("varchar")
                    .HasMaxLength(15);
            });
            builder.OwnsOne(a => a.Address, x =>
            {
                x.Property(a => a.StreetAddress)
                    .IsRequired()
                    .HasColumnType("varchar")
                    .HasMaxLength(80);
            });
            builder.HasOne(a => a.Image)
                .WithOne(a => a.User)
                .HasForeignKey<Images>(a => a.UserId);
            builder.HasQueryFilter(x => !x.IsRemoved);
            builder.HasQueryFilter(x => !x.IsSuspended);
        }
    }
}