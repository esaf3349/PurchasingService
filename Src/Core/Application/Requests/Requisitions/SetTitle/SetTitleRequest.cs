using MediatR;

namespace Application.Requests.Requisitions.SetTitle;

public sealed record SetTitleRequest : IRequest<Unit>
{
    public required Guid Id { get; init; }
    public required string Title { get; init; }
}