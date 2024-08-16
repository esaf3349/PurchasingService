using Application.Authorization.Common.Authorizers;
using Application.Authorization.MustBeLoggedIn;

namespace Application.Requests.Users.Search;

internal class SearchAuthorizer : BaseAuthorizer<SearchRequest>
{
    public override void BuildPolicy(SearchRequest request)
    {
        UseRequirement(new MustBeLoggedInRequirement());
    }
}