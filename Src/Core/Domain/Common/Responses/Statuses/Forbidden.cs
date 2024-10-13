namespace Domain.Common.Responses.Statuses;

public class Forbidden : IResponseStatus
{
    public Forbidden(string? message)
    {
        Message = message;
    }

    public string? Message { get; }
}