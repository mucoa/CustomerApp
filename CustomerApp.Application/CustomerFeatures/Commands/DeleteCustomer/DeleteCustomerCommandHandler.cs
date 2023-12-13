using CustomerApp.Application.Abstractions;
using CustomerApp.Application.Abstractions.Messaging;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace CustomerApp.Application.CustomerFeatures.Commands.DeleteCustomer;

internal sealed class DeleteCustomerCommandHandler : ICommandHandler<DeleteCustomerCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteCustomerCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _context.Customers
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
            .ConfigureAwait(false);

        if (customer is null) {
            return Result.Fail("User not found");
        }

        _context.Customers
            .Remove(customer);

        await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        return Result.Ok();
    }
}
