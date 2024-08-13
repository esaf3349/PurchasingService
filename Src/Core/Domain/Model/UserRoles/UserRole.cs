using Domain.Common.Entities;
using Domain.Model.Roles;
using Domain.Model.Users;

namespace Domain.Model.UserRoles;

public sealed class UserRole : BaseEntity<Guid>
{
    public Guid UserId { get; init; }
    public User? User { get; }
    public Guid RoleId { get; init; }
    public Role? Role { get; }

    private UserRole() { }

    public UserRole(Guid id, Guid userId, Guid roleId) : base(id)
    {
        UserId = userId;
        RoleId = roleId;
    }
}