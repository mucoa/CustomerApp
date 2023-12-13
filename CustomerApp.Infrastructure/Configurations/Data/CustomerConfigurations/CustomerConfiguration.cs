using Microsoft.EntityFrameworkCore;
using CustomerApp.Domain.Entities.Customers;
using CustomerApp.Domain.Enums;

namespace CustomerApp.Infrastructure.Configurations.Data.CustomerConfigurations;

internal sealed class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable(TableNames.Customer);

        builder.HasMany(x => x.Orders)
            .WithOne(x => x.Customer);
    }
}
