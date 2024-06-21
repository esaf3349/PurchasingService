using Domain.Common.Entities;
using Domain.Common.Exceptions;
using Domain.Model.Requisitions;
using Domain.Model.Users.Constants;

namespace Domain.Model.Users;

public sealed class User : BaseEntity<Guid>
{
    public string Login { get; private set; }
    public string? FirstName { get; private set; }
    public string? LastName { get; private set; }
    public string? MiddleName { get; private set; }

    public ICollection<Requisition> Requisitions { get; private set; }

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

    private void SetLogin(string login)
    {
        if (login.Length > LoginConstants.MaxLength)
            throw new DomainException<User>($"{nameof(Login)} should not be longer than {LoginConstants.MaxLength} symbols");

        if (login.Length < LoginConstants.MinLength)
            throw new DomainException<User>($"{nameof(Login)} should be at least {LoginConstants.MinLength} symbols");

        Login = login;
    }
}