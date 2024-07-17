namespace Application.Contracts.Presentation.CurrentUser.Dtos;

public sealed record User(Guid Id, string FirstName, string LastName, string MiddleName, string Login);