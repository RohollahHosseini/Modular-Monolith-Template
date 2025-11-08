using Blog.BuildingBlocks.Peresentation.ApiResult;
using Blog.BuildingBlocks.Peresentation.EndpointFilters.EndpointFilterAbstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Blog.BuildingBlocks.Peresentation.EndpointFilters
{
    public class ContentResultEndpointFilter : IApiEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            var result = await next(context);

            if (result is ContentHttpResult contentHttpResult)
            {
                var apiResult = new ApiResult.ApiResult(true, ApiResultStatusCode.Success, contentHttpResult.ResponseContent);
                return Results.Json(apiResult, statusCode: contentHttpResult.StatusCode ?? StatusCodes.Status200OK);
     
            }

            if (result is string stringResult)
            {
                var apiResult = new ApiResult.ApiResult(true, ApiResultStatusCode.Success, stringResult);
                return Results.Json(apiResult, statusCode: StatusCodes.Status200OK);
            }


            if (result is ApiResult.ApiResult or IResult)
                return result;

            var wrapper = new ApiResult<object>(true, ApiResultStatusCode.Success, result);
            return Results.Json(wrapper, statusCode: StatusCodes.Status200OK);
        }
    }
}
