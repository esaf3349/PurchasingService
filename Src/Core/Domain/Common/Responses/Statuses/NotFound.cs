namespace Domain.Common.Responses.Statuses;

public class NotFound : IResponseStatus
{
    public NotFound(string? message)
    {
        Message = message;
    }

    public string? Message { get; }
}