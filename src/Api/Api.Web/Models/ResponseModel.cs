using System;

namespace Api.Web
{
    public class ResponseModel<T>
    {
        public T Data { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }

        public ResponseModel(T data, string status = "success", string message = null)
        {
            Data = data;
            Status = status;
            Message = message;
            Timestamp = DateTime.UtcNow;
        }
    }

    public class ErrorResponseModel
    {
        public string Type { get; set; }
        public string Error { get; set; }
        public string Detail { get; set; }
        public DateTime Timestamp { get; set; }

        public ErrorResponseModel(string type, string error, string detail)
        {
            Type = type;
            Error = error;
            Detail = detail;
            Timestamp = DateTime.UtcNow;
        }
    }
}