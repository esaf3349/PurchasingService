namespace WebApi.Common.Http;

public sealed class JsonResponse<TData>
{
    public JsonResponse() { }

    public JsonResponse(string? message)
    {
        Message = message;
    }

    public JsonResponse(TData? data)
    {
        Data = data;
    }

    public string? Message { get; }
    public TData? Data { get; }
}