using Domain.Common.Responses.Statuses;

namespace Domain.Common.Responses;

public sealed class Response<TValue>
{
    private Response(TValue value, IResponseStatus status)
    {
        Value = value;
        Status = status;
    }

    public TValue Value { get; }
    public IResponseStatus Status { get; }

    public static Response<TValue> AlreadyExists(TValue value, string? message = null) =>
        new Response<TValue>(value, new AlreadyExists(message));

    public static Response<TValue> BadRequest(TValue value, string? message = null) =>
        new Response<TValue>(value, new BadRequest(message));

    public static Response<TValue> Forbidden(TValue value, string? message = null) =>
        new Response<TValue>(value, new Forbidden(message));

    public static Response<TValue> NotFound(TValue value, string? message = null) =>
        new Response<TValue>(value, new NotFound(message));

    public static Response<TValue> Success(TValue value, string? message = null) =>
        new Response<TValue>(value, new Success(message));

    public static Response<TValue> Unauthorized(TValue value, string? message = null) =>
        new Response<TValue>(value, new Unauthorized(message));
}