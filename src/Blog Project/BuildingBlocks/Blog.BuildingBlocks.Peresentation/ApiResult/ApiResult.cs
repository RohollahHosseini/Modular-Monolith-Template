using Blog.BuildingBlocks.SharedKernel.Extensions;
using System.Diagnostics;

namespace Blog.BuildingBlocks.Peresentation.ApiResult
{
    public class ApiResult(bool IsSuccess, ApiResultStatusCode statusCode,string? Message=null)
    {
        public bool IsSuccess { get; set; } = IsSuccess;
        public ApiResultStatusCode StatusCode { get; set; } = statusCode;
        public string Message { get; set; } = Message?? statusCode.ToDisplay();
        public string  RequestId { get;}=Activity.Current?.TraceId.ToHexString()??string.Empty;
    }

    public class ApiResult<TDate>(bool IsSuccess, ApiResultStatusCode statusCode,TDate Data,string? Message = null) : ApiResult(IsSuccess, statusCode, Message)
    {
        public TDate Data { get; set; }=Data;
    }

}
