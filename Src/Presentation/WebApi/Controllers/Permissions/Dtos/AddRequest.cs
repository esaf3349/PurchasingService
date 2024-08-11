using Domain.Model.Permissions.ValueObjects;

namespace WebApi.Controllers.Permissions.Dtos;

public sealed record AddRequest(AllowedEntity? EntityFilter, string? EntityIdFilter, string? PropertyFilter, AllowedAction? ActionFilter);