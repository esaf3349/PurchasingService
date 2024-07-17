using Application.Contracts.Presentation.CurrentUser.Dtos;
using System.Text.Json;
using WebApi.Services.CurrentUser.Constants;

namespace WebApi.Services.CurrentUser.Extensions;

internal static class SessionExtensions
{
    public static User? GetUser(this ISession session) =>
        session.Get<User?>(SessionConstants.CurrentUserSessionName);

    public static void SetUser(this ISession session, User? user) =>
        session.Set(SessionConstants.CurrentUserSessionName, user);

    private static TPayload Get<TPayload>(this ISession session, string sessionKey)
    {
        var sessionPayload = session.GetString(sessionKey);
        var deserializedPayload = JsonSerializer.Deserialize<TPayload>(sessionPayload);

        return deserializedPayload;
    }

    private static void Set<TPayload>(this ISession session, string sessionKey, TPayload payload)
    {
        var serializedPayload = JsonSerializer.Serialize(payload);

        session.SetString(sessionKey, serializedPayload);
    }
}