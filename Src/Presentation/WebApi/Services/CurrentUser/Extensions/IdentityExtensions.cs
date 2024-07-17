using Application.Exceptions;
using System.Security.Principal;

namespace WebApi.Services.CurrentUser.Extensions;

internal static class IdentityExtensions
{
    public static string GetAccountName(this IIdentity? identity)
    {
        if (identity == null)
            throw new UnauthorizedException("Unknown authentication scheme");

        var identityName = identity.Name;
        if (!identityName.Contains("\\"))
            throw new UnauthorizedException("Unknown account domain");

        var identityNameParts = identityName.Split("\\");
        if (identityNameParts.Length < 2 )
            throw new UnauthorizedException("Unknown account domain");

        var accountName = identityNameParts[1];
        if (string.IsNullOrWhiteSpace(accountName))
            throw new UnauthorizedException("Indalid account name");

        return accountName;
    }
}