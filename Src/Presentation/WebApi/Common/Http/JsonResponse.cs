namespace WebApi.Common.Http;

public sealed class JsonResponse<TData>
{
    private JsonResponse() { }

    private JsonResponse(string? message, TData? data)
    {
        Message = message;
        Data = data;
    }

    public string? Message { get; }
    public TData? Data { get; }

    public static JsonResponse<TData> Success(TData? data) => new JsonResponse<TData>(null, data);

    public static JsonResponse<TData> Error(string message) => new JsonResponse<TData>(message, default);
}