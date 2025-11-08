using Blog.BuildingBlocks.Peresentation.Endpoints;
using Blog.Modules.Content.Application.Features.Blog.Commands;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Blog.Modules.Content.Peresentation.Blog
{
    internal sealed class CreateBlog : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            var apies = app.MapGroup("/blog");

            apies.MapPost("/", async(CreateBlogCommand model, ISender sender )=> 
            {
                var command = await sender.Send(model);

                return command;
            });
        }
    }
}
