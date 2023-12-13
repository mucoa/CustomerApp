using CustomerApp.Domain.Entities.User;
using CustomerApp.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CustomerApp.Infrastructure.Configurations.Data;

internal sealed class PermissionConfiguration : IEntityTypeConfiguration<Permission>
{
    public void Configure(EntityTypeBuilder<Permission> builder)
    {
        builder.ToTable(TableNames.Permission);

        builder.HasKey(x => x.Id);

        IEnumerable<Permission> permissions = Enum.GetValues<Permissions>()
            .Select(x => new Permission
            {
                Id = (int)x,
                Name = x.ToString(),
                IsDisabled = false
            });

        builder.HasData(permissions);
    }
}
