using CustomerApp.Application.Abstractions.Messaging;
using CustomerApp.Application.Helpers;
using CustomerApp.Domain.Results.Customer;

namespace CustomerApp.Application.CustomerFeatures.Queries.GetOrders;

public sealed record GetOrdersQuery(string? SearchText,
    string? SortColumn,
    string? SortOrder,
    int Page,
    int PageSize) : IQuery<PagedList<GetOrderResult>>;
