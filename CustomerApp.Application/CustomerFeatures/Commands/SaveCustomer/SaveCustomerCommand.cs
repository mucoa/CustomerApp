using CustomerApp.Application.Abstractions.Messaging;

namespace CustomerApp.Application.CustomerFeatures.Commands.SaveCustomer;

public sealed record SaveCustomerCommand(string Identity,
    string Name,
    string PhoneNumber,
    string EmailAddress,
    string Address,
    DateTime BirthDate,
    string? Company) : ICommand;
