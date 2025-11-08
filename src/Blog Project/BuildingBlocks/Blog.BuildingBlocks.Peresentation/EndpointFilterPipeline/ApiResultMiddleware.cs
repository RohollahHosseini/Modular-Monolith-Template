using Blog.BuildingBlocks.Peresentation.ApiResult;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Blog.BuildingBlocks.Peresentation.EndpointFilterPipeline
{
    public class ApiResultMiddleware
    {
        private readonly RequestDelegate _next;

        public ApiResultMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // ذخیره Response اصلی
            var originalBodyStream = context.Response.Body;

            using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            try
            {
                await _next(context);

                // Reset position
                context.Response.Body.Seek(0, SeekOrigin.Begin);

                // خواندن محتوا
                var bodyText = await new StreamReader(context.Response.Body).ReadToEndAsync();

                context.Response.Body.Seek(0, SeekOrigin.Begin);

                object? apiResult = null;

                switch (context.Response.StatusCode)
                {
                    case StatusCodes.Status200OK:
                        apiResult = new ApiResult<object>(true, ApiResultStatusCode.Success, !string.IsNullOrEmpty(bodyText) ? JsonConvert.DeserializeObject<object>(bodyText) : null);
                        break;

                    case StatusCodes.Status400BadRequest:
                        apiResult = new ApiResult<object>(false, ApiResultStatusCode.BadRequest, !string.IsNullOrEmpty(bodyText) ? JsonConvert.DeserializeObject<object>(bodyText) : null);
                        break;

                    case StatusCodes.Status404NotFound:
                        apiResult = new ApiResult.ApiResult(false, ApiResultStatusCode.NotFound, !string.IsNullOrEmpty(bodyText) ? bodyText : null);
                        break;

                    case StatusCodes.Status500InternalServerError:
                        apiResult = new ApiResult.ApiResult(false, ApiResultStatusCode.ServerError, !string.IsNullOrEmpty(bodyText) ? bodyText : "Internal Server Error");
                        break;

                    default:
                        // برای سایر StatusCode ها می‌توان بدون تغییر عبور داد
                        await responseBody.CopyToAsync(originalBodyStream);
                        return;
                }

                context.Response.ContentType = "application/json";
                context.Response.Body = originalBodyStream;
                context.Response.StatusCode = context.Response.StatusCode;

                await context.Response.WriteAsJsonAsync(apiResult);
            }
            catch (Exception ex)
            {
                context.Response.Body = originalBodyStream;
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                var errorResult = new ApiResult.ApiResult(false, ApiResultStatusCode.ServerError, ex.Message);
                await context.Response.WriteAsJsonAsync(errorResult);
            }
        }
    }

    

}
