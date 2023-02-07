using AMP.Domain.Entities.BusinessManagement;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AMP.Persistence.Configurations.BusinessManagement;

public class PaymentWithdrawalConfiguration : DatabaseConfiguration<PaymentWithdrawal>
{
    public override void Configure(EntityTypeBuilder<PaymentWithdrawal> builder)
    {
        base.Configure(builder);
        builder.Ignore(x => x.IsMomo);
        builder.OwnsOne(x => x.MomoTransfer);
        builder.Property(x => x.UserId).IsRequired();
    }
}