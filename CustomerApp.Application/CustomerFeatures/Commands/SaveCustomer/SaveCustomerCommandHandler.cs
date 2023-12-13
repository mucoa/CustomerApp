using CustomerApp.Application.Abstractions;
using CustomerApp.Application.Abstractions.Messaging;
using CustomerApp.Domain.Entities.Customers;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace CustomerApp.Application.CustomerFeatures.Commands.SaveCustomer;

internal sealed class SaveCustomerCommandHandler : ICommandHandler<SaveCustomerCommand>
{
    private readonly IApplicationDbContext _context;

    public SaveCustomerCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result> Handle(SaveCustomerCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _context.Customers
            .AnyAsync(x => x.EmailAddress == request.EmailAddress || 
                x.Identity == request.Identity, cancellationToken)
            .ConfigureAwait(false);

        if (existingUser)
        {
            return Result.Fail("Existing user please, provide unique one.");
        }

        await _context.Customers
            .AddAsync(new Customer()
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                PhoneNumber = request.PhoneNumber,
                EmailAddress = request.EmailAddress,
                Address = request.Address,
                BirthDate = request.BirthDate,
                Company = request.Company,
                Identity = request.Identity,
                CreatedAt = DateTime.Now,
            }, cancellationToken)
            .ConfigureAwait(false);

        await _context.SaveChangesAsync(cancellationToken)
            .ConfigureAwait(false);

        return Result.Ok();
    }
}
