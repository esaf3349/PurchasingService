using Application.Contracts.Infra.Persistence;
using Domain.Common.Expressions;
using Domain.Model.Requisitions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Application.Requests.Requisitions.Search;

public sealed class SearchHandler : IRequestHandler<SearchRequest, IEnumerable<Requisition>>
{
    private readonly IUnitOfWork _uow;

    public SearchHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<IEnumerable<Requisition>> Handle(SearchRequest request, CancellationToken cancellationToken = default)
    {
        Expression<Func<Requisition, bool>> filter = r => r.IsActive;

        if (request.Number != null)
            filter = filter.And(r => r.Number == request.Number);

        if (!string.IsNullOrWhiteSpace(request.Title))
            filter = filter.And(r => r.Title.Contains(request.Title));

        if (request.Status != null)
            filter = filter.And(r => r.Status == request.Status);

        if (request.SupplierId != null)
            filter = filter.And(r => r.SupplierId == request.SupplierId);

        if (request.DepartmentId != null)
            filter = filter.And(r => r.DepartmentId == request.DepartmentId);

        if (request.RequesterId != null)
            filter = filter.And(r => r.RequesterId == request.RequesterId);

        if (request.DueDate != null)
            filter = filter.And(r => r.DueDate == request.DueDate);

        var persistedRequisitions = await _uow.Requisitions.Where(filter).ToArrayAsync(cancellationToken);

        return persistedRequisitions;
    }
}