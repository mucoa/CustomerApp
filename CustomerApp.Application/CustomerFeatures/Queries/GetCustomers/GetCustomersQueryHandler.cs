using CustomerApp.Application.Abstractions;
using CustomerApp.Application.Abstractions.Messaging;
using CustomerApp.Application.Helpers;
using CustomerApp.Domain.Entities.Customers;
using FluentResults;
using System.Linq.Expressions;

namespace CustomerApp.Application.CustomerFeatures.Queries.GetCustomers;

internal sealed class GetCustomersQueryHandler : IQueryHandler<GetCustomersQuery, PagedList<Customer>>
{
    private readonly IApplicationDbContext _context;

    public GetCustomersQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<PagedList<Customer>>> Handle(GetCustomersQuery request, 
        CancellationToken cancellationToken)
    {
        IQueryable<Customer> customersQuery = _context.Customers;

        if (!string.IsNullOrWhiteSpace(request.SearchText))
        {
            customersQuery = customersQuery.Where(x =>
            x.Identity.Contains(request.SearchText) ||
            x.Company.Contains(request.SearchText) ||
            x.EmailAddress.Contains(request.SearchText) ||
            x.Name.Contains(request.SearchText));
        }

        if (request.SortOrder?.ToLower() == "desc")
        {
            customersQuery = customersQuery.OrderByDescending(getSortOrder(request));
        }
        else
        {
            customersQuery = customersQuery.OrderBy(getSortOrder(request));
        }

        var customers = await PagedList<Customer>.CreateAsync(
                customersQuery,
                request.Page,
                request.PageSize,
                cancellationToken
            ).ConfigureAwait(false);

        return customers;
    }

    private Expression<Func<Customer, object>> getSortOrder(GetCustomersQuery request) => request.SortOrder?.ToLowerInvariant() switch
    {
        "birthdate"  => customer => customer.BirthDate,
        "company"  => customer => customer.Company,
        "email"  => customer => customer.EmailAddress,
        "identity"  => customer => customer.Identity,
        "name"  => customer => customer.Name,
        _ => customer => customer.CreatedAt,
    };
}
