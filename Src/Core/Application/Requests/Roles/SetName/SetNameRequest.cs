﻿using MediatR;

namespace Application.Requests.Roles.SetName;

public sealed record SetNameRequest : IRequest<Unit>
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
}