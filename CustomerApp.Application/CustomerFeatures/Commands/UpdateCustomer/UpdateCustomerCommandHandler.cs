using CustomerApp.Application.Abstractions;
using CustomerApp.Application.Abstractions.Messaging;
using CustomerApp.Domain.Entities.Customers;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace CustomerApp.Application.CustomerFeatures.Commands.UpdateCustomer;

internal sealed class UpdateCustomerCommandHandler : ICommandHandler<UpdateCustomerCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateCustomerCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
         var customer = await _context.Customers
            .Include(x => x.Orders)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
            .ConfigureAwait(false);

        if (customer is null) {
            return Result.Fail("User not found.");
        }

        customer.Identity = request.Identity;
        customer.Name = request.Name;
        customer.BirthDate = request.BirthDate;
        customer.PhoneNumber = request.PhoneNumber;
        customer.Address = request.Address;
        customer.Company = request.Company;
        customer.EmailAddress = request.EmailAddress;
        customer.CreatedAt = DateTime.Now;
        customer.Orders = request.Orders ?? new List<Order>();

        await _context.SaveChangesAsync(cancellationToken)
            .ConfigureAwait(false);

        return Result.Ok();
    }
}
