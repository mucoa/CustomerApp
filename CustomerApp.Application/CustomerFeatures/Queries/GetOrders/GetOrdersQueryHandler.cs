using CustomerApp.Application.Abstractions;
using CustomerApp.Application.Abstractions.Messaging;
using CustomerApp.Application.Helpers;
using CustomerApp.Domain.Entities.Customers;
using CustomerApp.Domain.Results.Customer;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CustomerApp.Application.CustomerFeatures.Queries.GetOrders;

internal sealed class GetOrdersQueryHandler : IQueryHandler<GetOrdersQuery, PagedList<GetOrderResult>>
{
    private readonly IApplicationDbContext _context;

    public GetOrdersQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<PagedList<GetOrderResult>>> Handle(GetOrdersQuery request,
        CancellationToken cancellationToken)
    {
        IQueryable<Order> orderQuery = _context.Orders
            .Include(x => x.Customer);

        if (!string.IsNullOrWhiteSpace(request.SearchText))
        {
            orderQuery = orderQuery.Where(x =>
                x.Customer.Name.Contains(request.SearchText) ||
                x.Product.Contains(request.SearchText));
        }

        if (request.SortOrder?.ToLower() == "desc") {
            orderQuery = orderQuery.OrderByDescending(getSortOrder(request));
        }
        else
        {
            orderQuery = orderQuery.OrderBy(getSortOrder(request));
        }

        var orderResultsQuery = orderQuery
            .Select(x => new GetOrderResult
            {
                CustomerName = x.Customer.Name,
                Date = x.CreatedAt,
                OrderNumber = x.OrderNumber,
                Price = x.ProductPrice,
                Product = x.Product
            });

        var orders = await PagedList<GetOrderResult>.CreateAsync(
            orderResultsQuery,
            request.Page,
            request.PageSize,
            cancellationToken
            ).ConfigureAwait(false);

        return orders;
    }

    private static Expression<Func<Order, object>> getSortOrder(GetOrdersQuery request) => request.SortColumn?.ToLower() switch
    {
        "number" => order => order.OrderNumber,
        "price" => order => order.ProductPrice,
        "product" => order => order.Product,
        "name" => order => order.Customer.Name,
        "date" => order => order.CreatedAt,
        _ => order => order.CreatedAt,
    };
}
