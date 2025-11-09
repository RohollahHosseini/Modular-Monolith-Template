using Blog.BuildingBlocks.Peresentation.EndpointFilters;
using Blog.BuildingBlocks.Peresentation.Endpoints;
using Blog.Modules.Content.Application.Features.Blog.Commands;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Blog.Modules.Content.Peresentation.Blog
{
    internal sealed class BlogEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            var apies = app.MapGroup("/blog")
                .WithTags("Blog")
                .AddEndpointFilter<ApiResultFilterAttribute>()
                .AddEndpointFilter<BadRequestResultEndpointFilter>()
                .AddEndpointFilter<ContentResultEndpointFilter>()
                .AddEndpointFilter<ModelStateValidationEndpointFilter>()
                .AddEndpointFilter<NotFoundResultEndpointFilter>()
                .AddEndpointFilter<OkResultEndpointFilter>();

            apies.MapPost("/", async(CreateBlogCommand model, ISender sender )=> 
            {
                var command = await sender.Send(model);

                return command;
            });
        }
    }
}
