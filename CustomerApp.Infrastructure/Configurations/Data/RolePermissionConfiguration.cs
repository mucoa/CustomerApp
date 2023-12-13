using CustomerApp.Domain.Entities.User;
using CustomerApp.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomerApp.Infrastructure.Configurations.Data;

internal sealed class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
{
    public void Configure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.ToTable(TableNames.RolePermission);

        builder.HasKey(x => new { x.RoleId, x.PermissionId });

        builder.HasData(
            Create(Role.Administrator, Permissions.CreateCustomer),
            Create(Role.Administrator, Permissions.UpdateCustomer),
            Create(Role.Administrator, Permissions.DeleteCustomer),
            Create(Role.Administrator, Permissions.GetOrders),
            Create(Role.StandartUser, Permissions.GetOrders)
        );
    }

    public static RolePermission Create(Role role,
        Permissions permission)
        => new()
        { RoleId = role.Id, PermissionId = (int)permission };
}
