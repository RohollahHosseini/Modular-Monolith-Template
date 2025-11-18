using Blog.BuildingBlocks.Peresentation.EndpointFilters;
using Blog.BuildingBlocks.Peresentation.Endpoints;
using Blog.BuildingBlocks.Peresentation.WebExtensions;
using Blog.Modules.Content.Application.Features.Category.Commands;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Blog.Modules.Content.Peresentation.Category
{
    internal class CategoryEndpoint : IEndpoint
    {
        public void MapEndpoint(IEndpointRouteBuilder app)
        {
            var apies = app.MapGroup("api/v1/Category")
               .WithTags("Category")
               .AddEndpointFilter<OkResultEndpointFilter>()
               .AddEndpointFilter<NotFoundResultEndpointFilter>()
               .AddEndpointFilter<BadRequestResultEndpointFilter>()
               .AddEndpointFilter<ModelStateValidationEndpointFilter>();

            apies.MapPost("/", async (CreateCategoryCommand model, ISender sender) =>
            {
                var result = await sender.Send(model);

                return result.ToEndpointResult();
            });
        }
    }
}
