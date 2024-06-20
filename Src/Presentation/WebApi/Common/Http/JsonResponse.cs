namespace WebApi.Common.Http;

public sealed class JsonResponse<TData>
{
    public string? Message { get; init; }
    public TData? Data { get; init; }

    public JsonResponse() { }

    public JsonResponse(string? message)
    {
        Message = message;
    }

    public JsonResponse(TData? data)
    {
        Data = data;
    }
}