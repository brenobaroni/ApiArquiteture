using System.Text.Json;

namespace Api.Domain.Models
{
    public class ResponseModel<T>
    {
        public bool success { get; set; }
        public string message { get; set; } = string.Empty;
        public T data { get; set; } = default!;
        public List<ErrorMessageModel> ErrorMessages { get; set; }
    }

    public class ErrorMessageModel
    {
        public string message { get; set; }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
