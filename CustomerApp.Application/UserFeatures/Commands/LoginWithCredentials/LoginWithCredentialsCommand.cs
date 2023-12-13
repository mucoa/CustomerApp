using CustomerApp.Application.Abstractions.Messaging;
using System.ComponentModel.DataAnnotations;

namespace CustomerApp.Application.UserFeatures.Commands.LoginWithCredentials;

public sealed record LoginWithCredentialsCommand(
    [DataType(DataType.EmailAddress)] string Email,  
    [DataType(DataType.Password)] string Password) : ICommand<string>;
