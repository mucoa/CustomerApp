using CustomerApp.Application.Abstractions;
using CustomerApp.Application.Abstractions.Messaging;
using CustomerApp.Domain.Entities.Customers;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace CustomerApp.Application.CustomerFeatures.Queries.GetCustomerById;

internal sealed class GetCustomerByIdQueryHandler : 
    IQueryHandler<GetCustomerByIdQuery, Customer>
{
    private readonly IApplicationDbContext _context;

    public GetCustomerByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Customer>> Handle(GetCustomerByIdQuery request, 
        CancellationToken cancellationToken)
    {
        var user = await _context.Customers
            .Include(x => x.Orders)
            .Where(x=> x.Id == request.Id)
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);

        if (user is null)
        {
            return Result.Fail("User cannot found.");
        }

        return Result.Ok(user);
    }
}
