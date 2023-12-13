using CustomerApp.Domain.Entities.Customers;
using Microsoft.EntityFrameworkCore;

namespace CustomerApp.Application.Abstractions;

public interface IApplicationDbContext
{
    DbSet<Customer> Customers { get; set; }
    DbSet<Order> Orders { get; set; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
