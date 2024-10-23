public class ApiResponse<T>
{
    public bool Success { get; set; }
    public string Message { get; set; } = "";
    public T Data { get; set; } = default!;
    public List<string> Errors { get; set; }
    public int StatusCode { get; set; }

    public ApiResponse()
    {
        Errors = new List<string>();
    }
}