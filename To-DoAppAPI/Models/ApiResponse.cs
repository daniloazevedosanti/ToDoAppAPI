namespace TodoApp.API.Models
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public ApiResponse(bool success, string message, T data)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        public static ApiResponse<T> Ok(T data, string message = "Operação realizada com sucesso")
        {
            return new ApiResponse<T>(true, message, data);
        }

        public static ApiResponse<T> Fail(string message, T data = default)
        {
            return new ApiResponse<T>(false, message, data);
        }
    }

    public class ApiResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }

        public ApiResponse(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public static ApiResponse Ok(string message = "Operação realizada com sucesso")
        {
            return new ApiResponse(true, message);
        }

        public static ApiResponse Fail(string message)
        {
            return new ApiResponse(false, message);
        }
    }
}