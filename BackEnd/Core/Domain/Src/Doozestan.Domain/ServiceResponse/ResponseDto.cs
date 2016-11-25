using Doozestan.Domain.Enum;

namespace Doozestan.Domain.ServiceResponse
{
    public class ResponseDto<T>
    {
        public T Data { get; set; }
        public ApiResponseStatus ResponseStatus { get; set; }
        public string ResponseMessage { get; set; }
        public int Total { get; set; }
    }
}