using Application.Contracts.Infra.Persistence;
using Domain.Common.Expressions;
using Domain.Model.Suppliers;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Application.Requests.Suppliers.Search;

public sealed class SearchHandler : IRequestHandler<SearchRequest, IEnumerable<Supplier>>
{
    private readonly IUnitOfWork _uow;

    public SearchHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<IEnumerable<Supplier>> Handle(SearchRequest request, CancellationToken cancellationToken = default)
    {
        Expression<Func<Supplier, bool>> filter = s => s.IsActive;

        if (!string.IsNullOrWhiteSpace(request.Name))
            filter = filter.And(s => s.Name.Contains(request.Name));

        if (!string.IsNullOrWhiteSpace(request.Description))
            filter = filter.And(s => s.Description.Contains(request.Description));

        var persistedSuppliers = await _uow.Suppliers.Where(filter).ToArrayAsync(cancellationToken);

        return persistedSuppliers;
    }
}