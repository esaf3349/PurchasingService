namespace Domain.Common.Responses.Statuses;

public class Unauthorized : IResponseStatus
{
    public Unauthorized(string? message)
    {
        Message = message;
    }

    public string? Message { get; }
}