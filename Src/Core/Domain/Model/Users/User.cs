using Domain.Common.Entities;
using Domain.Common.Exceptions;
using Domain.Common.Guids;
using Domain.Model.EntityChanges;
using Domain.Model.Requisitions;
using Domain.Model.Roles;
using Domain.Model.UserRoles;
using Domain.Model.Users.Constants;
using System.Text.RegularExpressions;

namespace Domain.Model.Users;

public sealed class User : BaseEntity<Guid>
{
    public string Login { get; private set; }
    public string? FirstName { get; private set; }
    public string? LastName { get; private set; }
    public string? MiddleName { get; private set; }
    public string? Email { get; private set; }

    private readonly HashSet<UserRole> _userRoles = [];

    public ICollection<Requisition> Requisitions { get; private set; }
    public ICollection<EntityChange> EntityChanges { get; private set; }
    public ICollection<UserRole> UserRoles => _userRoles;

    private User() { }

    public User(Guid id, string login) : base(id)
    {
        SetLogin(login);
    }

    public void SetFirstName(string? firstName)
    {
        if (firstName?.Length > FirstNameConstants.MaxLength)
            throw new DomainException<User>($"{nameof(FirstName)} should not be longer than {FirstNameConstants.MaxLength} symbols");

        FirstName = firstName;
    }

    public void SetLastName(string? lastName)
    {
        if (lastName?.Length > LastNameConstants.MaxLength)
            throw new DomainException<User>($"{nameof(LastName)} should not be longer than {LastNameConstants.MaxLength} symbols");

        LastName = lastName;
    }

    public void SetMiddleName(string? middleName)
    {
        if (middleName?.Length > MiddleNameConstants.MaxLength)
            throw new DomainException<User>($"{nameof(MiddleName)} should not be longer than {MiddleNameConstants.MaxLength} symbols");

        MiddleName = middleName;
    }

    public void SetEmail(string? email)
    {
        if (!Regex.IsMatch(email, @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9._-]+\.[a-zA-Z]{2,4}$"))
            throw new DomainException<User>($"{nameof(Email)} is invalid");

        if (email?.Length > EmailConstants.MaxLength)
            throw new DomainException<User>($"{nameof(Email)} should not be longer than {EmailConstants.MaxLength} symbols");

        Email = email;
    }

    public UserRole AddRole(Role role)
    {
        var existingUserRole = _userRoles.FirstOrDefault(ur => ur.RoleId == role.Id && ur.IsActive);
        if (existingUserRole != null)
            throw new DomainException<User>($"Role {role.Id} already exists");

        var newUserRole = new UserRole(AppGuid.New, Id, role.Id);
        _userRoles.Add(newUserRole);

        return newUserRole;
    }

    public void RemoveRole(Guid roleId)
    {
        var userRole = _userRoles.FirstOrDefault(ur => ur.RoleId == roleId && ur.IsActive);
        if (userRole == null)
            throw new DomainException<User>($"Role {roleId} doesn't exist");

        userRole.Delete();
    }

    private void SetLogin(string login)
    {
        if (login.Length > LoginConstants.MaxLength)
            throw new DomainException<User>($"{nameof(Login)} should not be longer than {LoginConstants.MaxLength} symbols");

        if (login.Length < LoginConstants.MinLength)
            throw new DomainException<User>($"{nameof(Login)} should be at least {LoginConstants.MinLength} symbols");

        Login = login;
    }
}