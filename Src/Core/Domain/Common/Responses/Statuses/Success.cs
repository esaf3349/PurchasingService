namespace Domain.Common.Responses.Statuses;

public class Success : IResponseStatus
{
    public Success(string? message)
    {
        Message = message;
    }

    public string? Message { get; }
}