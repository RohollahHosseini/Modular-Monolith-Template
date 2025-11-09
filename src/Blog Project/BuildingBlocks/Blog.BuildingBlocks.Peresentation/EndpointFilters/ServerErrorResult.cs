using Blog.BuildingBlocks.Peresentation.ApiResult;
using Blog.BuildingBlocks.Peresentation.EndpointFilters.EndpointFilterAbstract;
using Blog.BuildingBlocks.SharedKernel.Extensions;
using Microsoft.AspNetCore.Http;

namespace Blog.BuildingBlocks.Peresentation.EndpointFilters
{
    public class ServerErrorResult(string Message) : IApiEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
			try
			{
                var result = await next(context);

                if (result is IStatusCodeHttpResult statusCodeResult &&
                statusCodeResult.StatusCode == StatusCodes.Status500InternalServerError)
                {
                    string? message = null;

                    if (result is IValueHttpResult valueHttp && valueHttp.Value is string str)
                    {
                        message = str;
                    }

                    var apiResult = new ApiResult.ApiResult(
                        false,
                        ApiResultStatusCode.ServerError,
                        message ?? ApiResultStatusCode.ServerError.ToDisplay()
                    );

                    return Results.Json(apiResult, statusCode: StatusCodes.Status500InternalServerError);
                }

                return result;

            }
			catch (Exception ex)
			{

                var apiResult = new ApiResult.ApiResult(
                    false,
                    ApiResultStatusCode.ServerError,
                    ex.Message
                );

                return Results.Json(apiResult, statusCode: StatusCodes.Status500InternalServerError);
            }

        }
    }
}
