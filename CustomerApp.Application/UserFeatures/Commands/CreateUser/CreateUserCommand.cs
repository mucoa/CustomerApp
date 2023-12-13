using CustomerApp.Application.Abstractions.Messaging;
using System.ComponentModel.DataAnnotations;

namespace CustomerApp.Application.UserFeatures.Commands.CreateUser;

public sealed record CreateUserCommand(
    [DataType(DataType.EmailAddress)] string Email,
    [DataType(DataType.Password)] string Password) : ICommand; 
