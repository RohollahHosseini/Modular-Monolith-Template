using Microsoft.AspNetCore.Builder;

namespace Blog.BuildingBlocks.Peresentation.EndpointFilterPipeline
{
    public static class ApiResultMiddlewareExtensions
    {
        public static IApplicationBuilder UseApiResultMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ApiResultMiddleware>();
        }
    }
}
