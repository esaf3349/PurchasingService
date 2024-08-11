using Application.Contracts.Infra.Persistence;
using Domain.Common.Expressions;
using Domain.Model.Roles;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Application.Requests.Roles.Search;

public sealed class SearchHandler : IRequestHandler<SearchRequest, IEnumerable<Role>>
{
    private readonly IUnitOfWork _uow;

    public SearchHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<IEnumerable<Role>> Handle(SearchRequest request, CancellationToken cancellationToken = default)
    {
        Expression<Func<Role, bool>> filter = r => r.IsActive;

        if (!string.IsNullOrWhiteSpace(request.Name))
            filter = filter.And(r => r.Name.Contains(request.Name));

        if (!string.IsNullOrWhiteSpace(request.Description))
            filter = filter.And(r => r.Description.Contains(request.Description));

        if (request.ForSingleUser != null)
            filter = filter.And(r => r.ForSingleUser == request.ForSingleUser);

        var persistedRoles = await _uow.Roles
            .Include(r => r.Permissions.Where(p => p.IsActive))
            .Where(filter).ToArrayAsync(cancellationToken);

        return persistedRoles;
    }
}