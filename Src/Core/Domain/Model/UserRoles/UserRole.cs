using Domain.Common.Entities;
using Domain.Model.Roles;
using Domain.Model.Users;

namespace Domain.Model.UserRoles;

public sealed class UserRole : BaseEntity<Guid>
{
    public Guid UserId { get; }
    public User? User { get; }
    public Guid RoleId { get; }
    public Role? Role { get; }

    private UserRole() { }
}