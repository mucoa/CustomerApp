using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using CustomerApp.Infrastructure.Configurations.Options;
using CustomerApp.Infrastructure.Authentication;
using CustomerApp.Application.Authentication;
using CustomerApp.Infrastructure.Miscellaneous.Login;
using CustomerApp.Application.Abstractions.User;
using CustomerApp.Infrastructure.UserRepositories;
using CustomerApp.Application.Abstractions;

namespace CustomerApp.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<ApplicationDbContext>(
        (serviceProvider, dbContextOptionsBuilder) =>
        {
            var databaseOptions = serviceProvider.GetService<IOptions<DatabaseOption>>()!.Value;

            var connectionString = databaseOptions.ConnectionString;

            dbContextOptionsBuilder.UseSqlServer(connectionString, sqlServerOptionsAction =>
            {
                sqlServerOptionsAction.EnableRetryOnFailure(databaseOptions.MaxRetryCount);

                sqlServerOptionsAction.CommandTimeout(databaseOptions.CommandTimeout);
            });

            dbContextOptionsBuilder.EnableDetailedErrors(databaseOptions.EnabledDetailErrors);

            dbContextOptionsBuilder.EnableSensitiveDataLogging(databaseOptions.EnabledSensetiveDataLogging);
        });

        services.AddTransient<IPermissionService, PermissionService>();
        services.AddTransient<IJwtProvider, JwtProvider>();
        services.AddTransient<IPasswordHasher, PasswordHasher>();
        services.AddTransient<IUserRepository, UserRepository>();

        services.AddScoped<IApplicationDbContext>(services => services.GetRequiredService<ApplicationDbContext>());

        return services;
    }
}
