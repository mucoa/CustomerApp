using CustomerApp.Domain.Entities.User;
using CustomerApp.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomerApp.Infrastructure.Configurations.Data;

internal sealed class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.ToTable(TableNames.UserRole);

        builder.HasKey(x => new { x.UserId, x.RoleId });
    }
}
