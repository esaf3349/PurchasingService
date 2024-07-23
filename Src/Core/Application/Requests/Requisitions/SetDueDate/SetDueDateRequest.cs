using MediatR;

namespace Application.Requests.Requisitions.SetDueDate;

public sealed record SetDueDateRequest : IRequest<Unit>
{
    public required Guid Id { get; init; }
    public required DateTime DueDate { get; init; }
}