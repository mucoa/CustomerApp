using CustomerApp.Application.Abstractions.Messaging;
using CustomerApp.Domain.Entities.Customers;

namespace CustomerApp.Application.CustomerFeatures.Commands.UpdateCustomer;

public sealed record UpdateCustomerCommand(Guid Id,
    string Identity,
    string Name,
    string PhoneNumber,
    string EmailAddress,
    string Address,
    DateTime BirthDate,
    string? Company,
    ICollection<Order>? Orders) : ICommand;
