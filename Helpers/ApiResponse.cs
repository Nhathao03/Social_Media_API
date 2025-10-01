namespace Social_Media.Helpers
{
    public class ApiResponse<T>
    {
        private static string GetMesageFromStatusCode(int statusCode) => statusCode switch
        {
            200 => "OK",
            201 => "Created",
            204 => "No Content",
            400 => "Bad Request",
            401 => "Unauthorized",
            403 => "Forbidden",
            404 => "Not Found",
            500 => "Internal Server Error",
            _ => "Unknown Status Code"
        };
        public int StatusCode { get; set; }
        public bool Success => StatusCode >= 200 && StatusCode < 300;
        public string? Message { get; set; }
        public T? Data { get; set; }
        public ApiResponse(int statusCode, string? message = null, T? data = default)
        {
            StatusCode = statusCode;
            Message = message ?? GetMesageFromStatusCode(statusCode);
            Data = data;
        }
    }
}