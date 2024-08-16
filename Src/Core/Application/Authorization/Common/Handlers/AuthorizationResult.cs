namespace Application.Authorization.Common.Handlers;

internal sealed class AuthorizationResult
{
    private AuthorizationResult() { }

    private AuthorizationResult(bool isSuccess, string? message)
    {
        IsSuccess = isSuccess;
        Message = message;
    }

    public bool IsSuccess { get; }
    public string? Message { get; }

    public static AuthorizationResult Success => new AuthorizationResult(true, null);

    public static AuthorizationResult Error(string message) => new AuthorizationResult(false, message);
}