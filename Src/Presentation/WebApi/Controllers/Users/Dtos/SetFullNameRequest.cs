namespace WebApi.Controllers.Users.Dtos;

public sealed record SetFullNameRequest(string FirstName, string LastName, string? MiddleName);