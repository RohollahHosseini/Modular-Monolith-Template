using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
