namespace Domain.Common.Responses.Statuses;

public class AlreadyExists : IResponseStatus
{
    public AlreadyExists(string? message)
    {
        Message = message;
    }

    public string? Message { get; }
}