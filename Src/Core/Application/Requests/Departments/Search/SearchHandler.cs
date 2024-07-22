using Application.Contracts.Infra.Persistence;
using Domain.Common.Expressions;
using Domain.Model.Departments;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Application.Requests.Departments.Search;

public sealed class SearchHandler : IRequestHandler<SearchRequest, IEnumerable<Department>>
{
    private readonly IUnitOfWork _uow;

    public SearchHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<IEnumerable<Department>> Handle(SearchRequest request, CancellationToken cancellationToken = default)
    {
        Expression<Func<Department, bool>> filter = s => s.IsActive;

        if (!string.IsNullOrWhiteSpace(request.Name))
            filter = filter.And(s => s.Name.Contains(request.Name));

        var persistedDepartments = await _uow.Departments.Where(filter).ToArrayAsync(cancellationToken);

        return persistedDepartments;
    }
}