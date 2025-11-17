using Blog.BuildingBlocks.Peresentation.EndpointFilters;
using Blog.BuildingBlocks.Peresentation.Endpoints;
using Blog.Modules.Content.Application.Features.Blog.Commands;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Blog.BuildingBlocks.Peresentation.WebExtensions;

namespace Blog.Modules.Content.Peresentation.Blog
{
    internal sealed class BlogEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            var apies = app.MapGroup("api/v1/blog")
                .WithTags("Blog")
                .AddEndpointFilter<OkResultEndpointFilter>()
                .AddEndpointFilter<NotFoundResultEndpointFilter>()
                .AddEndpointFilter<BadRequestResultEndpointFilter>()
                .AddEndpointFilter<ModelStateValidationEndpointFilter>();

            apies.MapPost("/", async(CreateBlogCommand model, ISender sender )=> 
            {
                var result = await sender.Send(model);

                return result.ToEndpointResult();
            });
        }
    }
}
