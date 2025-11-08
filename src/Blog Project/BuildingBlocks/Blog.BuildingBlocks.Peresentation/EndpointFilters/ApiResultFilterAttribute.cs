using Blog.BuildingBlocks.Peresentation.ApiResult;
using Blog.BuildingBlocks.Peresentation.EndpointFilters.EndpointFilterAbstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.BuildingBlocks.Peresentation.EndpointFilters
{
    public class ApiResultFilterAttribute : IApiEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var result = await next(context);


            if (result is IStatusCodeHttpResult statusCodeResult)
            {
                var statusCode = statusCodeResult.StatusCode;

                // ✅ OK (200)
                if (statusCode == StatusCodes.Status200OK)
                {
                    if (result is IValueHttpResult valueHttp)
                    {
                        var apiResult = new ApiResult<object>(true, ApiResultStatusCode.Success, valueHttp.Value);
                        return Results.Json(apiResult, statusCode: StatusCodes.Status200OK);
                    }

                    var apiResultOk = new ApiResult.ApiResult(true, ApiResultStatusCode.Success);
                    return Results.Json(apiResultOk, statusCode: StatusCodes.Status200OK);
                }

                // ✅ BadRequest (400)
                if (statusCode == StatusCodes.Status400BadRequest)
                {
                    string? message = null;
                    if (result is IValueHttpResult badRequestValue && badRequestValue.Value is SerializableError errors)
                    {
                        var errorMessages = errors.SelectMany(p => (string[])p.Value).Distinct();
                        message = string.Join(" | ", errorMessages);
                    }
                    else if (result is IValueHttpResult badRequestString && badRequestString.Value is string str)
                    {
                        message = str;
                    }

                    var apiResult = new ApiResult.ApiResult(false, ApiResultStatusCode.BadRequest, message ?? "Bad Request");
                    return Results.Json(apiResult, statusCode: StatusCodes.Status400BadRequest);
                }

                // ✅ NotFound (404)
                if (statusCode == StatusCodes.Status404NotFound)
                {
                    if (result is IValueHttpResult notFoundValue)
                    {
                        var apiResult2 = new ApiResult<object>(false, ApiResultStatusCode.NotFound, notFoundValue.Value);
                        return Results.Json(apiResult2, statusCode: StatusCodes.Status404NotFound);
                    }

                    var apiResult = new ApiResult.ApiResult(false, ApiResultStatusCode.NotFound);
                    return Results.Json(apiResult, statusCode: StatusCodes.Status404NotFound);
                }

                // ✅ Server Error (500)
                if (statusCode == StatusCodes.Status500InternalServerError)
                {
                    string? message = null;
                    if (result is IValueHttpResult errorResult && errorResult.Value is string msg)
                        message = msg;

                    var apiResult = new ApiResult.ApiResult(false, ApiResultStatusCode.ServerError, message ?? "Internal Server Error");
                    return Results.Json(apiResult, statusCode: StatusCodes.Status500InternalServerError);
                }
            }

            // ✅ 2. برای نتایجی که IStatusCodeHttpResult نیستند ولی داده دارند
            if (result is not ApiResult.ApiResult && result is not IResult)
            {
                var apiResult = new ApiResult<object>(true, ApiResultStatusCode.Success, result);
                return Results.Json(apiResult, statusCode: StatusCodes.Status200OK);
            }

            return result;

        }
    }
}
