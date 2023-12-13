using CustomerApp.Domain.Entities.User;
using CustomerApp.Domain.Enums;
using CustomerApp.Infrastructure.Miscellaneous.Login;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations;

namespace CustomerApp.Infrastructure.Configurations.Data;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(TableNames.User);
        builder.HasKey(x => x.UserId);

        builder.HasMany(x => x.Roles)
            .WithMany()
            .UsingEntity<UserRole>();

    }
}
