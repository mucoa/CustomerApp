using CustomerApp.Application.Abstractions.User;
using CustomerApp.Domain.Entities.User;
using CustomerApp.Infrastructure.Authentication;
using CustomerApp.Infrastructure.Miscellaneous.Login;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CustomerApp.Infrastructure.UserRepositories;

internal sealed class UserRepository(ApplicationDbContext context,
    IPasswordHasher passwordHasher,
    IJwtProvider jwtProvider) : IUserRepository
{
    private readonly ApplicationDbContext _context = context;
    private readonly IPasswordHasher _passwordHasher = passwordHasher;
    private readonly IJwtProvider _jwtProvider = jwtProvider;

    public async Task<Result> Create(
        [DataType(DataType.EmailAddress)] string Email, 
        [DataType(DataType.Password)] string Password, 
        CancellationToken cancellationToken)
    {
        var user = await _context.Set<User>()
            .Where(x => x.UserEmail == Email)
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);

        if (user != null) {
            return Result.Fail("This account cannot be created!");
        }

        await _context.Set<User>()
            .AddAsync(new() { 
                UserId = Guid.NewGuid(),
                UserEmail = Email,
                Password = _passwordHasher.Hash(Password),
                IsActive = true,
            }, cancellationToken)
            .ConfigureAwait(false);

        var result = await _context.SaveChangesAsync(cancellationToken)
            .ConfigureAwait(false);
      
        if(result != 1)
        {
            return Result.Fail("User could not created.");
        }

        return Result.Ok();
    }

    public async Task<Result<string>> Login(
        [DataType(DataType.EmailAddress)] string Email,
        [DataType(DataType.Password)] string Password,
        CancellationToken cancellationToken)
    {

        var user = await _context.Set<User>()
            .Where(x => x.UserEmail == Email)
            .FirstOrDefaultAsync(cancellationToken)
            .ConfigureAwait(false);

        if (user is null)
        {
            return Result.Fail("Provided creadentials are not valid.");
        }

        if (!_passwordHasher.Verify(Password, user.Password))
        {
            return Result.Fail("Provided creadentials are not valid.");
        }

        string token = await _jwtProvider.Generate(user).ConfigureAwait(false);

        return Result.Ok(token);
    }
}
