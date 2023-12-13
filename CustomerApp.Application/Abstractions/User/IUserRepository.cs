using FluentResults;
using System.ComponentModel.DataAnnotations;

namespace CustomerApp.Application.Abstractions.User;

public interface IUserRepository
{
    public Task<Result<string>> Login(
        [DataType(DataType.EmailAddress)] string Email,
        [DataType(DataType.Password)] string Password, 
        CancellationToken cancellationToken);

    public Task<Result> Create(
        [DataType(DataType.EmailAddress)] string Email,
        [DataType(DataType.Password)] string Password, 
        CancellationToken cancellationToken);
}
