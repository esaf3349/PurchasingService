using Application.Contracts.Infra.Persistence;
using Domain.Common.Expressions;
using Domain.Model.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Application.Requests.Users.Search;

public sealed class SearchHandler : IRequestHandler<SearchRequest, IEnumerable<User>>
{
    private readonly IUnitOfWork _uow;

    public SearchHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<IEnumerable<User>> Handle(SearchRequest request, CancellationToken cancellationToken = default)
    {
        Expression<Func<User, bool>> filter = s => s.IsActive;

        if (!string.IsNullOrWhiteSpace(request.Login))
            filter = filter.And(s => s.Login.Contains(request.Login));

        if (!string.IsNullOrWhiteSpace(request.FirstName))
            filter = filter.And(s => s.FirstName.Contains(request.FirstName));

        if (!string.IsNullOrWhiteSpace(request.LastName))
            filter = filter.And(s => s.LastName.Contains(request.LastName));

        if (!string.IsNullOrWhiteSpace(request.MiddleName))
            filter = filter.And(s => s.MiddleName.Contains(request.MiddleName));

        if (!string.IsNullOrWhiteSpace(request.Email))
            filter = filter.And(s => s.Email.Contains(request.Email));

        var persistedSuppliers = await _uow.Users.Where(filter).ToArrayAsync(cancellationToken);

        return persistedSuppliers;
    }
}