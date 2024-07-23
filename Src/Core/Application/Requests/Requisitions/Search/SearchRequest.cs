using Domain.Model.Requisitions;
using Domain.Model.Requisitions.ValueObjects;
using MediatR;

namespace Application.Requests.Requisitions.Search;

public sealed record SearchRequest : IRequest<IEnumerable<Requisition>>
{
    public required int? Number { get; init; }
    public required string? Title { get; init; }
    public required Status? Status { get; init; }
    public required Guid? SupplierId { get; init; }
    public required Guid? DepartmentId { get; init; }
    public required Guid? RequesterId { get; init; }
    public required DateTime? DueDate { get; init; }
}