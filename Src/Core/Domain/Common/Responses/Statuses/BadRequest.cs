namespace Domain.Common.Responses.Statuses;

public class BadRequest : IResponseStatus
{
    public BadRequest(string? message)
    {
        Message = message;
    }

    public string? Message { get; }
}