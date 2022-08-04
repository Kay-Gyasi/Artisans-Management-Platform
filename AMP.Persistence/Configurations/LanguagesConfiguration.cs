using AMP.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AMP.Persistence.Configurations
{
    public class LanguagesConfiguration : DatabaseConfigurationBase<Languages>
    {
        public override void Configure(EntityTypeBuilder<Languages> builder)
        {
            builder.Property(a => a.Name).IsRequired();
            base.Configure(builder);
        }
    }
}